#include <sys/types.h>
#include <sys/wait.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>
#include <signal.h>
#include <pthread.h>

struct cln {
    int cfd;
    struct sockaddr_in caddr;
};

struct user_data{
    char* name; //nazwa uzytkownika
    int logged; //czy jest zalogowany
    char* password; //haslo uzytkownika
    int index_forum; //forum do ktorego nalezy uzytkownik. Potrzebne przy zmianie forum
    int id;
};
struct forum_data{
    int id_users[9]; //id socketu
    char username[9][64]; //nazwa uzytkownikow bedacych na forum
    int countUser; //ilosc uzytkownikow na forum
    char* name; //nazwa forum
};

int set=0;
size_t len=0;
//nazwy plikow zawierajacych historie kazdego serwera
char historyForum[10][11] = { 
    "forum0.txt", "forum1.txt", "forum2.txt", "forum3.txt", "forum4.txt",
    "forum5.txt", "forum6.txt", "forum7.txt", "forum8.txt", "forum9.txt" 
};
struct user_data users[9];
struct forum_data forums[10];

//funkcja wyswietlajaca nazwe forum, ilosc zalogowanych na niego uzytkownikow oraz ID i nazwe uzytkownikow [debug]
void showForum(){
    for(int i=0;i<10;i++){
        printf("%d: %s %d\n", i, forums[i].name, forums[i].countUser); 
        for(int j=0; j<forums[i].countUser;j++){
            printf("---%d>%s<\n", forums[i].id_users[j], forums[i].username[j]);
        }
    }
}
//funkcja zapisujaca liste forow
void saveForumToFile(){
    FILE *listForum=fopen("fileForum.txt", "w");
    fclose(listForum);
    listForum=fopen("fileForum.txt", "a");
    for(int i=0;i<10;i++){
        fprintf(listForum, "%s\n", forums[i].name);
    }
    fclose(listForum);
}

//funkcja inicjalizacyjna serwer; wczytywanie plikow zewnetrznych
void setup(){
    FILE *fileUser=fopen("fileUser.txt", "rt");
    FILE *fileForum=fopen("fileForum.txt", "rt");
    set=1;
    len=0;
    for( int i=0;i<9;i++){
        users[i].logged = 0;
        users[i].id = -1;
        getline(&users[i].name, &len, fileUser);
        strtok(users[i].name, "\n"); 
        users[i].index_forum = -1;
    }
    for( int i=0;i<9;i++){
        getline(&users[i].password, &len, fileUser);
        strtok(users[i].password, "\n"); 
    }
    for( int i=0;i<10;i++){
        forums[i].countUser = 0;
        for(int j=0; j<9;j++){
            forums[i].id_users[j]=0;
            strcpy(forums[i].username[j], "w");
        }
        getline(&forums[i].name, &len, fileForum);
        strtok(forums[i].name, "\n");
    }
    fclose(fileUser);
    fclose(fileForum);
}
//funkcja dzieki ktorej podany uzytkownik zostaje wylogowany z forum na ktorym jest zalogowany
//usuwanie uzytkownika polega na zamienienie pozycją wybranego użytkownika i ostatniego zalogowanego. Nastepnie dane struktury dot. uzytkownika są czyszczone
void leaveForum(int index_user, int cfd){
    for(int j=0; j<9;j++){  
        int indexOfForum = users[index_user].index_forum;
        if( forums[indexOfForum].id_users[j] == cfd ){
            forums[indexOfForum].id_users[j] = forums[indexOfForum].id_users[forums[indexOfForum].countUser-1];
            forums[indexOfForum].id_users[forums[indexOfForum].countUser-1] = 0;
            strcpy(forums[indexOfForum].username[j], forums[indexOfForum].username[forums[indexOfForum].countUser-1]);
            strcpy(forums[indexOfForum].username[forums[indexOfForum].countUser-1],"w");
            forums[indexOfForum].countUser--;
            j=9; 
        }
    }
}
//funkcja wysylajaca wiadomosci do klienta
void sendMessage(int cfdd, char text[1024]){
    int counter = 0;
    int written = 0;
    while( counter<strlen(text)){
        written = write(cfdd ,text, strlen(text));  
        counter+=written;
    }
}

//funkcja porownujaca ciagi znakow
int compare(char* str1, char* str2){
    int i=0;
    while(str1[i]!='\0' && str2[i]!='\0'){
        if(str1[i]!=str2[i]){
            return 0;
        }
        else if(str1[i]=='\0'){  
            return 1;
        }
        i++;
    }
    if(str1[i]!=str2[i]){
        return 0;
    } 
    return 1;
}

//obsluga watku
void* cthread(void* arg){
    char msgFromClient[1024]; //tutaj zapisywana jest wiadomosc przyslana od klienta
    int ifCorrectName; //flaga uzywana do wysylania komunikatow do klienta nt. poprawnosci wykonywanych operacji (np. poprawnosc loginu i hasla)
    char msgToClient[1024]; //tu zapisywana jest odpowiedz na zapytanie klienta
    char login[256] = {'w'}; //zmienna przechowujaca login uzytkownika
    char password[256] = {'w'};//zmienna przechowujaca haslo uzytkownika
    int counter;
    int index_user=-1; //informacja o uzytkoniku - index w strukturze uzytkownikow
    int countNewLine = 0; //licznik znakow nowej linii \n
    int logged; //flaga, mowiaca czy uzytkownik jest zalogowany
    char flag;  //typ odbieranej wiadomosci od klienta
    int size=0;
    int i=0;
    int password_iter, login_iter; //zmienne sluzace do zapisu loginu i hasla z wiadomosci od klienta
    logged = 0;   
    struct cln* c = (struct cln*)arg;
    //funkcja logowania
    while(logged==0){
        //dopoki nie odczyta dwoch znakow nowej linii; Odczytywanie znak po znaku
        //oczekiwany ciag znakow: 
        //     1\njeden\n
        //   [login]\n[haslo]\n
        while(countNewLine <2){
            read(c->cfd, msgFromClient, 1);
            if (countNewLine == 0 && msgFromClient[0] != '\n'){ //zapis loginu
                login[login_iter] = msgFromClient[0];
                login_iter++;
            }
            if (countNewLine == 1 && msgFromClient[0] != '\n'){ //zapis hasla
                password[password_iter] = msgFromClient[0];
                password_iter++;
            }
            if ( msgFromClient[0] == '\n'){
                countNewLine++;
            } 
        }
        //odszukanie w bazie zarejestrowanych uzytkownikow
        for( int i=0;i<9;i++){
            if(compare(login, users[i].name) && (users[i].logged==0)){
                if(compare(password, users[i].password)){
                    //gdy login i haslo sie zgadzaja
                    users[i].logged = 1;
                    logged=1;
                    users[i].id = c->cfd;
                    index_user = i;
                    printf("Uzytkownik %s zalogowany\n", login);
                    countNewLine=0;
                    sendMessage(c->cfd, "0Y\n"); //wyslanie informacji zwrotnej do klienta
                    i=9; 
                }
            }
        }
        if(logged==0){
            printf("Blad logowania\n");
            sendMessage(c->cfd, "0N\n");
            countNewLine=0;
        }
    }
    //uzytkownik zalogowany:
    while(logged==1){
        countNewLine=0;
        size =0;
        i=0;
        msgFromClient[0]='\0';
        //odczytywanie wiadomosci od serwera.
        // przykladowa oczekiwana wiadomosc
        //   1\n5\nAdres
        //  [typ_wiadomosci]\n[ilosc_znakow_tresci]\n[tresc]
        
        while(countNewLine <2){
            read(c->cfd, msgFromClient, 1);
            if ( msgFromClient[0] == '\n'){
                countNewLine++;
            }
            if( countNewLine == 0){
                flag = msgFromClient[0]; //odczytanie typu
            }
            if (countNewLine != 0 && msgFromClient[0] != '\n'){ //odczytanie ilosci znakow
                size = size * 10 + msgFromClient[0] - '0';
            }
        }
        countNewLine=0;
        msgFromClient[0]='\0';
        counter=0;
        while(i < size){ //odczytanie wiadomosci o okreslonej liczbie znakow
            counter = read(c->cfd, msgFromClient+i, 1024);
            i+= counter;
        }
        msgFromClient[size] = '\0';
        printf("Nowa wiadomosc: %c %s %d\n", flag, msgFromClient, size);
       
        /*
        * 1 - wybierz(dolacz) forum 
        * 2 - napisz na forum
        * 3 - wyloguj
        * 4 - utworz forum
        * 5 - usun forum
        * 6 - napisz do uzytkownika
        * 7 - zwroc liste forum
        * 8 - zwroc liste zalogowanych na forum uzytkownikow
        * 
        */
        switch( flag ){
            case '1':
                printf("Opcja1 - Dolacz do forum\n");
                ifCorrectName=0;
                for(int i=0;i<10;i++){
                    if(forums[i].name[0] != '-'){ //jesli forum nie jest puste
                        if(compare( forums[i].name, msgFromClient)){ //jesli nazwa forum zgadza sie z nazwa podana przez klienta
                            forums[i].id_users[ forums[i].countUser] = c->cfd; //zapisanie id do wybranego forum
                            if(users[index_user].index_forum != -1){ //jeżeli użytkownik jest w jakimś forum
                                for(int j=0;j< forums[users[index_user].index_forum].countUser;j++){
                                    int cfdd = forums[users[index_user].index_forum].id_users[j];
                                    sendMessage(cfdd, "e\n"); //wyslij wszystkim obecnym uzytkownikom, ktorzy sa na forum przed jego zmiana, aby odswiezyli liste uzytkownikow
                                }
                                leaveForum(index_user, c->cfd); //usun uzytkownika z biezacego forum
                            }
                            users[index_user].index_forum = i; //przypisz indeks forum do uzytkownika
                            strcpy(forums[i].username[forums[i].countUser],users[index_user].name); //zapisz uzytkownika na ostatnie wolne miejsce
                            forums[i].countUser++;
                            ifCorrectName=1;
                            i=10;
                            sendMessage(c->cfd, "cY\n"); //informacja do klienta o sukcesie
                            char* historyMessege;
                            //odczyt pliku z historia
                            FILE *hist = fopen( historyForum[ users[index_user].index_forum], "rt");
                            printf("Wysylanie historii forum\n");
                            int first=0;
                            for(int j=0;j< forums[users[index_user].index_forum].countUser;j++){
                                int cfdd = forums[users[index_user].index_forum].id_users[j];
                                sendMessage(cfdd, "e\n");
                            }
                            //wysylanie zawartosci forum nie jest wysylane raz. Wysylane są po kolei wiadomosci. Pierwsza wiadomosc z flaga 1 oznacza, ze klient ma wykasowac pole czatu, natomiast 2 oznacza, ze ja ma dopisac.
                            while(getline(&historyMessege, &len, hist) >0){ 
                                msgToClient[0] = '\0';
                                if(first==0){
                                    strcat(msgToClient, "1");
                                    first=1;
                                }
                                else
                                    strcat(msgToClient, "2");
                                strcat(msgToClient, historyMessege);
                                strcat(msgToClient, "\n\n");
                                printf("%s %ld\n", msgToClient, strlen(msgToClient) );
                                sendMessage(c->cfd, msgToClient); //wyslanie do klienta historii
                            }
                            printf("Wyslano historie\n");
                            fclose(hist);
                        }
                    }
                }
                if(ifCorrectName==0){
                    printf("nie ma takiego forum\n");
                    sendMessage(c->cfd, "cN\n");
                }
                printf("Zakonczono operacje 1\n");
                showForum();
                break;
            case '2':
                printf("Opcja2 - napisz na forum\n");
                //wyslanie do uczestnikow forum wiadomosci. Wiadomosc zostaje zapisana w pliku historii serwera
                for(int i=0;i<forums[ users[index_user].index_forum].countUser;i++){
                    int cfdd = forums[ users[index_user].index_forum ].id_users[i];
                    msgToClient[0]='\0';
                    strcat(msgToClient, "2");
                    strcat(msgToClient, users[index_user].name);
                    strcat(msgToClient, "\t");
                    strcat(msgToClient, msgFromClient);
                    strcat(msgToClient, "\n");
                    sendMessage(cfdd, msgToClient);
                    FILE *history = fopen( historyForum[users[index_user].index_forum], "a"); //zapis wiadomosci w pliku historii
                    fprintf(history, "%s\t%s\n", users[index_user].name, msgFromClient);
                    fclose(history);
                }
                printf("Wyslano wiadomosc na forum\n");
                msgFromClient[0]='\0';
                break;
                
            case '3':
                printf("Opcja3 - Wyloguj\n");
                //nastepuje wylogowanie z wyslaniem wiadomosci potwierdzającej klientowi
                if(users[index_user].index_forum!=-1){
                    for(int j=0;j< forums[ users[index_user].index_forum ].countUser;j++){
                        int cfdd = forums[users[index_user].index_forum].id_users[j];
                        sendMessage(cfdd, "e\n");
                    }   
                }
                users[index_user].logged =0;
                users[i].id = -1;
                logged = 0;
                index_user=-1;
                printf("czy to t\n");
                //leaveForum(index_user, c->cfd);
                
                printf("czy to ttttt\n");
                showForum();
                sendMessage(c->cfd, "3\n");
                printf("Wylogowano\n"); 
                break;
            case '4':
                //utworzenie nowego serwera
                ifCorrectName=1;
                printf("Opcja4 - Utworz nowe forum\n");
                for(int i=0;i<10;i++){
                    if( compare( forums[i].name, msgFromClient )){ //kiedy forum i podanej nazwie juz istnieje, operacja zostaje przerwana
                        ifCorrectName=0;
                        i=10;
                        printf("Istenieje juz  takie forum\n");
                        sendMessage(c->cfd, "bN\n"); //informacja dla klienta o bledzie
                    }
                }
                if(ifCorrectName==1){ //utworzenie nowego forum wiaze sie z utworzeniem pustego pliku historii serwera oraz wpisaniem tam jego nazwy
                    for(int i=0;i<10;i++){
                        if(forums[i].name[0] == '-'){
                            FILE *history = fopen( historyForum[i], "w");
                            sendMessage(c->cfd, "bY\n"); //informacja o sukcesie
                            strcpy( forums[i].name, msgFromClient);
                            fprintf(history, "%s\n", msgFromClient);
                            fclose(history);
                            i=10;
                            for(int j=0;j< 9;j++){
                                if(users[j].logged==1){
                                    int cfdd = users[j].id;
                                    sendMessage(cfdd, "f\n");
                                }
                            }
                        }
                    }
                }
                showForum();
                saveForumToFile();
                printf("Zakończono proces dodawania forum\n");
                break;
            case '5':
                //usuwanie forum. Nastepuje czyszczenie pliku z historia. Wszyscy uzytkownicy są usuwani z forum
                ifCorrectName = 0;
                printf("Opcja5 - Usuwanie forum\n");
                for(int i=0;i<10;i++){
                    if(compare( forums[i].name, msgFromClient)){
                        FILE *history = fopen( historyForum[i], "w");
                        strcpy( forums[i].name, "-");
                        for(int j=0;j<forums[i].countUser;j++){
                            //wyslanie do uzytkownikow forum informacji, ze forum przestalo istniec
                            int cfdd = forums[i].id_users[j];
                            sendMessage(cfdd, "a\n");
                            //ustawienie w strukturze forum, iż nikt nie jest do niego podlaczony
                            forums[i].id_users[j] = 0;
                            forums[i].username[j][0] = '\0';
                        }
                        for(int j=0;j< 9;j++){
                            if(users[j].logged==1){
                                int cfdd = users[j].id;
                                sendMessage(cfdd, "f\n");
                            }
                        }
                        if( i == users[index_user].index_forum){
                            users[index_user].index_forum=-1;
                        }
                        fclose(history);
                        sendMessage(c->cfd, "dY\n"); //informacja, ze pomyslnie usunieto forum
                        for(int j=0;j<9;j++){
                            if( users[j].index_forum == i ){
                                users[j].index_forum = -1;   
                            }
                        }
                        forums[i].countUser=0;
                        ifCorrectName=1;
                    }
                }
                if(ifCorrectName==0){
                    printf("nie ma takiego forum %d\n", i);
                    sendMessage(c->cfd, "dN\n"); //informacja o bledzie
                }
                saveForumToFile();
                showForum();
                printf("Zakończono proces usuwania forum\n");
                break;
            case '6':
                //wyslanie wiadomosci prywatnej do osoby zalogowanej na tym samym forum
                printf("Opcja6 - Wiadomosc prywatna\n");
                i=0;
                msgToClient[0]='\0';
                char* send_to;
                char* mess;
                strcat(msgToClient, "2PW FROM ");
                strcat(msgToClient, users[index_user].name);
                strcat(msgToClient, "\t");
                send_to = strtok( msgFromClient, "\t");
                mess = strtok(NULL, "\t");
                ifCorrectName=0;
                strcat(msgToClient, mess);
                strcat(msgToClient, "\n");
                int cfdd;
                for(int j=0; j< forums[ users[index_user].index_forum  ].countUser;j++){
                    if( compare( send_to,forums[ users[index_user].index_forum  ].username[j])){
                        ifCorrectName=1;
                        j=10;
                        sendMessage(c->cfd, "cY\n"); //informacja o sukcesie
                        cfdd = forums[ users[index_user].index_forum  ].id_users[j];
                        sendMessage(cfdd, msgToClient); //wyslanie wiadomosci na czat adresata
                    }
                }
                strcat(msgToClient, "2PW TO ");
                strcat(msgToClient, send_to);
                strcat(msgToClient, "\t");
                strcat(msgToClient, mess);
                strcat(msgToClient, "\n");
                sendMessage(cfdd, msgToClient); //wyslanie wiadomosci na czat nadawcy
                
                if(ifCorrectName==0){
                    printf("Nie ma takiego uzytkownika\n");
                    sendMessage(c->cfd, "cN\n");
                }
                printf("Zakończono proces prywatnej wiadomosci\n");
                break;
            case '7':
                //zwrocenie nazw dostepnych forów
                printf("Opcja7\n");
                msgToClient[0] = '\0';
                strcat(msgToClient, "7");
                for( int i=0;i<10; i++){
                    if( forums[i].name[0] != '-' ){
                        strcat(msgToClient, forums[i].name);
                        strcat(msgToClient, "\t");
                    }
                }
                strcat(msgToClient, "\n");
                sendMessage(c->cfd, msgToClient);
                printf("Wyslano liste forum\n");
            
                break;
            case '8':
                //zwrocenie listy zalogowanych uzytkownikow na forum
                printf("Opcja8\n");
                msgToClient[0]='\0';
                strcat(msgToClient, "8");
                showForum();
                
                if(users[index_user].index_forum!=-1){
                    for(int i=0;i<forums[users[index_user].index_forum].countUser;i++){
                        strcat(msgToClient, forums[users[index_user].index_forum].username[i]);
                        strcat(msgToClient, "\t");
                    }
                }
                else{
                    printf("uzytkownik niezalogowany\n");
                    
                    
                }
                strcat(msgToClient, " \n");
                sendMessage(c->cfd, msgToClient);
                printf("Wyslano liste uzytkownikow\n");
                break;
        }
    }
    close(c->cfd);
    free(c);
    return EXIT_SUCCESS;
}
int main(int argc, char** argv){
    if(set==0)
        setup();
    pthread_t tid;
    socklen_t slt;
    int sfd, on=1;
    struct sockaddr_in saddr;
    saddr.sin_family = AF_INET;
    saddr.sin_addr.s_addr = INADDR_ANY;
    saddr.sin_port = htons(1234);
    sfd = socket(AF_INET, SOCK_STREAM, 0);
    setsockopt(sfd, SOL_SOCKET, SO_REUSEADDR, (char*)&on, sizeof(on));
    bind(sfd, (struct sockaddr*)&saddr, sizeof(saddr));
    listen(sfd, 5);
    while(1){
        struct cln* c = malloc(sizeof(struct cln));
        slt = sizeof(c->caddr);
        c->cfd = accept(sfd, (struct sockaddr*)&c->caddr, &slt);
        printf("Nowe polaczenie\n");
        pthread_create(&tid, NULL, cthread, c);
        pthread_detach(tid);
    }
    close(sfd);
    return EXIT_SUCCESS;
}
    
    
