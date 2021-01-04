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
    char* name;
    int logged;
    char* password;
    int index_forum; //forum do ktorego nalezy uzytkownik. Potrzebne przy zmianie forum
};
struct forum_data{
    int id_users[9];
    char username[9][64];
    int countUser;
    char* name;
};


int set=0;

size_t len=0;
char historyForum[10][11] = { "forum0.txt", "forum1.txt", "forum2.txt", "forum3.txt", "forum4.txt", "forum5.txt", "forum6.txt", "forum7.txt", "forum8.txt", "forum9.txt" };
struct user_data users[9];
struct forum_data forums[10];

void showForum(){
    for(int i=0;i<10;i++){
        printf("%d: %s %d\n", i, forums[i].name, forums[i].countUser); 
        for(int j=0; j<forums[i].countUser;j++){
            printf("---%d\n", forums[i].id_users[j]);
        }
    }
}

void setup(){
    FILE *fileUser=fopen("fileUser.txt", "rt");
    FILE *fileForum=fopen("fileForum.txt", "rt");
    set=1;
    len=0;
    for( int i=0;i<9;i++){
        users[i].logged = 0;
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
void leaveForum(int index_user, int cfd){
    for(int j=0; j<9;j++){  
        if( forums[users[index_user].index_forum].id_users[j] == cfd ){
            forums[users[index_user].index_forum].id_users[j] = forums[users[index_user].index_forum].id_users[forums[users[index_user].index_forum].countUser];
            forums[users[index_user].index_forum].id_users[forums[users[index_user].index_forum].countUser] = 0;
            
            
            strcpy(forums[users[index_user].index_forum].username[j], forums[users[index_user].index_forum].username[forums[users[index_user].index_forum].countUser]);
            strcpy(forums[users[index_user].index_forum].username[forums[users[index_user].index_forum].countUser],"w");
            
            forums[users[index_user].index_forum].countUser--;
            
            j=9; 
        }
    }
}



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


void* cthread(void* arg){
    char buffer[1024];
    char login[256] = {'w'};
    char password[256] = {'w'};
    int counter = 0;
    int written = 0;
    int index_user;
    int countNewLine = 0;
    int logged;
    char flag;
    int size=0;
    int i=0;
    int password_iter, login_iter;
    logged = 0;
    //funkcja logowania
    struct cln* c = (struct cln*)arg;
    while(logged==0){
        while(countNewLine <2){
            read(c->cfd, buffer, 1);
            //printf("tekstf: %d %d %s %s %c\n",countNewLine,w, login, password, buffer[0]);
            if (countNewLine == 0 && buffer[0] != '\n'){
                login[login_iter] = buffer[0];
                login_iter++;
            }
            if (countNewLine == 1 && buffer[0] != '\n'){
                password[password_iter] = buffer[0];
                password_iter++;
            }
            if ( buffer[0] == '\n'){
                countNewLine++;
            } 
        }
        for( int i=0;i<9;i++){
            if(compare(login, users[i].name)){
                if(compare(password, users[i].password)){
                    users[i].logged = 1;
                    logged=1;
                    index_user = i;
                    printf("zalogowano\n");
                }
            }
        }
        if(logged==0){
            printf("blad logowania\n");
        }
    }
    
    while(logged==1){
        countNewLine=0;
        size =0;
        memset(buffer,0, strlen(buffer));
        while(countNewLine <2){
            read(c->cfd, buffer, 1);
            if ( buffer[0] == '\n'){
                countNewLine++;
            }
            if( countNewLine == 0){
                flag = buffer[0];
            }
            if (countNewLine != 0 && buffer[0] != '\n'){
                size = size * 10 + buffer[0] - '0';
            }
            
        }
        countNewLine=0;
        
        memset(buffer,0, strlen(buffer));
        while(i < size){
            counter = read(c->cfd, buffer+i, 1024);
            printf("Bufor: %c %s %d %ld\n", flag, buffer, size, strlen(buffer));
            i+= counter;
        }
        buffer[size] = '\0';
        printf("Bufor: %c %s %d\n", flag, buffer, size);
        
        i=0;
        // flag - typ komunikatu
        //buffer - tresc
        
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
                printf("Opcja1\n");
                for(int i=0;i<10;i++){
                    if(forums[i].name[0] != '-'){
                        if(compare( forums[i].name, buffer)){
                            forums[i].id_users[ forums[i].countUser] = c->cfd;
                            //jeżeli użytkownik jest w jakimś forum
                            if(users[index_user].index_forum != -1){
                                forums[users[index_user].index_forum].countUser--;
                                //usun uzytkownika z biezacego forum
                                
                                leaveForum(index_user, c->cfd);
                                
                                
                                
                            
                            
                            }
                            users[index_user].index_forum = i;
                            printf("AAAAAAA%s\n", forums[i].username[forums[i].countUser]);
                            printf("%s\n",users[index_user].name);
                            
                            strcpy(forums[i].username[forums[i].countUser],users[index_user].name);
                            
                            
                            forums[i].countUser++;
                            i=10;
                            
                            char* historyMessege;
                            FILE *hist = fopen( historyForum[ users[index_user].index_forum], "rt");
                            int cfdd;
                            printf("wysylanie...\n");
                            while(getline(&historyMessege, &len, hist) >0){
                                cfdd = c->cfd;
                                counter=0;
                                printf("%s %ld\n", historyMessege, strlen(historyMessege) );
                                while( counter<strlen(historyMessege)){
                                    written = write(cfdd,historyMessege, strlen(historyMessege));  
                                    counter+=written;
                                }
                            }
                            printf("Wyslano\n");
                            write(cfdd, "\n",1); 
                            fclose(hist);
                            
                        }
                    }
                }
                
                showForum();
                
                
                break;
            case '2':
                printf("Opcja2\n");
                for(int i=0;i<forums[ users[index_user].index_forum].countUser;i++){
                    counter = 0;
                    written = 0;
                    int cfdd = forums[ users[index_user].index_forum ].id_users[i];
                    printf("cfd: %d %d\n", counter, size);
                    while( counter<size){
                    printf("wyslano1 %s\n", buffer);
                        written = write(cfdd,buffer, size);
                        counter+=written;
                        printf("%d %d\n", written, counter);
                    }
                    write(cfdd, "\n", 1);
                    printf("wyslano\n");
                    FILE *history = fopen( historyForum[users[index_user].index_forum], "a");
                    fprintf(history, "%s\n", buffer);
                    fclose(history);
                    
                    strcpy(buffer, "");
                }
                break;
                
            case '3':
                printf("Opcja3\n");
                users[index_user].logged =0;
                logged = 0;
                leaveForum(index_user, c->cfd);
                showForum();
                break;
            case '4':
                printf("Opcja4\n");
                for(int i=0;i<10;i++){
                    if(forums[i].name[0] == '-'){
                        FILE *history = fopen( historyForum[i], "w");
                        strcpy( forums[i].name, buffer);
                        fprintf(history, "%s\n", buffer);
                        fclose(history);
                        i=10;
                    }
                }
                showForum();
                printf("dodano\n");
                break;
            case '5':
                printf("Opcja5\n");
                for(int i=0;i<10;i++){
                    printf("< %s %s >\n", forums[i].name, buffer);
                    if(compare( forums[i].name, buffer)){
                        counter = 0;
                        written = 0;
                        FILE *history = fopen( historyForum[i], "w");
                        strcpy( forums[i].name, "-");
                        for(int j=0;j<forums[i].countUser;j++){
                            /*int cfdd = forums[i].id_users[j];
                            while( counter<15){
                                written = write(cfdd,"Forum usunieto\n", 15);  
                                counter+=written;
                            }*/
                            forums[i].id_users[j] = 0;
                        }
                        fclose(history);
                        
                        for(int j=0;j<9;j++){
                            if( users[j].index_forum == i ){
                                users[j].index_forum = -1;   
                            }
                        }
                        i=10;
                    }
                }
                showForum();
                printf("usunieto\n");
                break;
            case '6':
                printf("Opcja6\n");
                i=0;
                char* send_to;
                char* mess;
                counter = 0;
                written = 0;
                send_to = strtok( buffer, "\t");
                mess = strtok(NULL, "\t");
                for(int j=0; j< forums[index_user].countUser;j++){
                    printf("< %s %s >\n", send_to, forums[index_user].username[j]);
                    if( compare( send_to, forums[index_user].username[j])){
                        printf("weszet\n");
                        int cfdd = forums[index_user].id_users[j];
                        while( counter<strlen(mess)){
                            written = write(cfdd,mess, strlen(mess));  
                            counter+=written;
                        }
                        write(cfdd, "\n", 1);
                    }
                }
                break;
            case '7':
                printf("Opcja7\n");
                counter = 0;
                written = 0;
                for( int i=0;i<10; i++){
                    counter = 0;
                    written = 0;
                    if( forums[i].name[0] != '-' ){
                        while( counter<strlen(forums[i].name)){
                            
                            printf("forum: %s\n", forums[i].name);
                            written = write(c->cfd ,forums[i].name, strlen(forums[i].name));  
                            counter+=written;
                        }
                        write(c->cfd ,"\t", 1);
                    }
                }
                write(c->cfd ,"\n", 1);
                break;
            case '8':
                printf("Opcja8\n");
                for(int i=0;i<forums[index_user].countUser;i++){
                    counter = 0;
                    written = 0;
                    while( counter<strlen(forums[index_user].username[i])){
                        written = write(c->cfd ,forums[index_user].username[i], strlen(forums[index_user].username[i]));  
                        counter+=written;
                    }
                    write(c->cfd ,"\t", 1);
                }
                write(c->cfd ,"\n", 1);
                
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
    struct sockaddr_in saddr, caddr;
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
        pthread_create(&tid, NULL, cthread, c);
        pthread_detach(tid);
    }
    close(sfd);
    return EXIT_SUCCESS;
    
}
    
    
