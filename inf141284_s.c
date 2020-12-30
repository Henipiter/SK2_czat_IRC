#include <stdio.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <string.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <signal.h>

struct msgbuf{
    long type;
    char text[1024];
    char text2[1024];
    int number;
    long sender;
};
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


int main(int args, char* argv[]){
    
    FILE *in=fopen("in.txt", "rt");
    size_t len=0;
    int p=1;
    
    const int MEMORY = 2048+sizeof(long)*2+sizeof(int)+1;
    struct msgbuf odebrana;
    struct msgbuf wyslana;
    const int PERMIT=3; //ile nieudanych logowan
    int banned[9][12]; 
    int mid=msgget(0x123, 0600 | IPC_CREAT);
    int mid2=msgget(0x124, 0600 | IPC_CREAT);
    char* users_name[9];
    int users_logged[9]; 
    long pid_users[9]; 
    int lock[9]; 
    char* users_password[9];
    int group_users[3][9]; 
    char* name_group[3];
    for(int j=0;j<9;j++){
        users_logged[j]=0;
        lock[j]=0;
        for(int k=0;k<12;k++){
            banned[j][k]=0;
        }
    }
//////////////////////////wczytywanie
    for(int i=0;i<9;i++){
        getline(&users_name[i], &len, in);
        strtok(users_name[i], "\n"); 
    }
    for(int i=0;i<9;i++){
        getline(&users_password[i], &len, in);
        strtok(users_password[i], "\n");
    }
    for(int i=0;i<3;i++){
        getline(&name_group[i], &len, in);
        strtok(name_group[i], "\n");
    }
    char* inicial;
    for(int i=0;i<3;i++){
        
        getline(&inicial, &len, in);
        //printf("\n%d\n", i);
        
        for(int j=0;j<9;j++){
            group_users[i][j]=inicial[j]-'0';
            //printf("%d,",j);
        }
    }
    fclose(in);
    ///koniec wczytywania
   
     pid_t serverPID = fork();
    if(!serverPID){
        while(p){
        msgrcv(mid, &odebrana, MEMORY, 10, 0);
            //////zaloguj//////
            if(odebrana.number==1){
                printf("Logowanie\n");
                int num=0;
                int flag=1;
                while(num<9 && flag){ //znalezienie uzytkownika
                    if(compare(odebrana.text, users_name[num])){ 
                        flag=0;
                    }
                    num++;
                }
                if(users_logged[num-1]){
                    
                    wyslana.text[0]='4';
                    printf("Nieudane\n");
                }
                
                else if(flag==1){
                    wyslana.text[0]='3';
                    printf("Nieudane\n");
                }
                else if(!compare(users_password[num-1], odebrana.text2)){ //sprawdzenie hasla. Jezeli rózne
                    lock[num-1]++;
                    if(lock[num-1]==3){
                    
                        wyslana.text[0]='q';
                    }
                    else{
                        wyslana.text[0]='0';
                    }
                    wyslana.number=PERMIT-lock[num-1];
                    printf("Nieudane\n");
                }
                else{
                    if(lock[num-1]>=3){
                        wyslana.text[0]='q';
                        printf("Nieudane\n");
                    }
                    else{
                        users_logged[num-1]=1;
                        pid_users[num-1]=odebrana.sender;
                        lock[num-1]=0;
                        wyslana.text[0]='1';
                        printf("Uzytkownik %s zalogowany\n", users_name[num-1]);
                    }
            
                }
                wyslana.type=odebrana.sender;
                msgsnd(mid, &wyslana, MEMORY, 0);
            }
            ///////wyloguj////////
            if(odebrana.number==2){
                printf("Wylogowanie\n");
                int i=0;
                while(pid_users[i]!=odebrana.sender){
                    i++;            
                }
                pid_users[i]=0;
                users_logged[i]=0;
                wyslana.type=odebrana.sender;
                strcpy(wyslana.text, "Wylogowano");
                msgsnd(mid, &wyslana, MEMORY, 0);
            }
            ////////wyswietl zalogowanych//////
            if(odebrana.number==3){
                printf("Zalogowani lista\n");
                for(int i=0;i<9;i++){
                    if(users_logged[i]==1){
                        strcpy(wyslana.text, users_name[i]);
                        wyslana.number=1;
                        msgsnd(mid, &wyslana, MEMORY, 0);
                    }
                    else{
                        wyslana.number=0;
                        msgsnd(mid, &wyslana, MEMORY, 0);
                    }
                }
            }
            ////////wyswietl grupy//////
            if(odebrana.number==4){
                printf("Wyswietl grupy\n");
                wyslana.type=odebrana.sender;
                for(int i=0;i<3;i++){
                    strcpy(wyslana.text,name_group[i]);  
                    msgsnd(mid, &wyslana, MEMORY, 0);
                    for(int j=0;j<9;j++){
                        if(group_users[i][j]==1){
                            strcpy(wyslana.text, users_name[j]);
                            wyslana.number=1;
                            msgsnd(mid, &wyslana, MEMORY, 0);
                        }
                        else{
                            wyslana.number=0;
                            msgsnd(mid, &wyslana, MEMORY, 0);
                        }
                    }
                }
            }
            ////////wyslij wiadomosc do uzytkownika///////////
            if(odebrana.number==5){
                printf("Wiadomosc do uzyt\n");
                
                int i=0;
                int pid;
                while(i<9){
                    if(pid_users[i]==odebrana.sender){
                        pid=i;
                        break;
                    }
                    i++;
                }
                int index;
                i=0;
                while(i<9){
                    if(compare(odebrana.text2, users_name[i])){
                        index=i; 
                    }
                    i++;
                }
                
                if(index<9 && users_logged[index]==1){
                    if(banned[index][pid]){
                            strcpy(wyslana.text,"Zablokowane wysylanie do tego uzytkownika");
                            printf("Wiadomosc zablokowana przez odbiorce.\n");
                    }
                    else{
                        wyslana.type=pid_users[index];
                        strcpy(wyslana.text2, users_name[pid]);
                        strcpy(wyslana.text, odebrana.text);
                        msgsnd(mid2, &wyslana, MEMORY, 0);
                        strcpy(wyslana.text,"Wyslano"); 
                        printf("Wyslano wiadomosc.\n");
                    }
                
                    wyslana.type=odebrana.sender;
                    msgsnd(mid, &wyslana, MEMORY, 0);
                    
                }           
                else{
                    strcpy(wyslana.text,"Odbiorca niezalogowany");
                    msgsnd(mid, &wyslana, MEMORY, 0);
                    printf("Niezalgowany odbiorca.\n");
                }
            }
            ///////wyslanie do grupy////
            if(odebrana.number==6){
                printf("Wiadomosc do grupy\n");
                int index;
                int i=0;
                int g=0;
                while(i<3){
                    if(compare(odebrana.text2, name_group[i])){
                        index=i;
                        break;
                    }
                    i++;
                }
                while(pid_users[g]!=odebrana.sender){
                    g++;
                }
                if(i<3 && group_users[index][g]){
                    for(int j=0;j<9;j++){
                        
                        if(users_logged[j]==1 && group_users[index][j]==1 && banned[j][index+9]==0){ //jezeli jest w grupie i jest zalogowany
                            wyslana.type=pid_users[j];
                            strcpy(wyslana.text2, name_group[index]);
                            strcpy(wyslana.text, odebrana.text);
                            msgsnd(mid2, &wyslana, MEMORY, 0);
                            wyslana.type=odebrana.sender;
                            
                            printf("Wyslano wiadomosc z grupy %s\n", name_group[index]);
                        }    
                    }
                    
                            strcpy(wyslana.text,"Wyslano");
                            msgsnd(mid, &wyslana, MEMORY, 0);
                }   
                else{
                    wyslana.type=odebrana.sender;
                    strcpy(wyslana.text,"Grupa nie istnieje lub do niej nie nalezysz");
                    msgsnd(mid, &wyslana, MEMORY, 0);
                    printf("Grupa nie istnieje lub uzytkownik nie nalezy do niej");
                }
            }
            ///////zapisz do grupy////
            if(odebrana.number==7){
                printf("Zapis do grupy\n");
                int i=0;
                while(pid_users[i]!=odebrana.sender){
                    i++;
                }
                printf("i=%d\n",i);
                int j=0;
                int t=1;
                while(t && j<3){
                    
                printf("nazwa %s|\n",odebrana.text);
                printf("nazwAa %s|\n",name_group[j]);
                    if(compare(odebrana.text, name_group[j]))
                        t=0;
                    j++;
                }
                
                printf("j=%d, t=%d\n",j,t);
                if(j==4){
                    strcpy(wyslana.text, "Nie ma takiej grupy\n"); 
                    printf("Nie ma takiej grupy\n");
                }
                else{
                    group_users[j-1][i]=1;
                    strcpy(wyslana.text, "Dodano do grupy\n");
                    printf("Dodano %s do grupy %s\n", users_name[i], name_group[j-1]);
                }
                wyslana.type=odebrana.sender;
                msgsnd(mid, &wyslana, MEMORY, 0);
            }
            ///////wypisz z grupy////
            if(odebrana.number==8){
                printf("Wypis z grupy\n");
                int i=0;
                while(pid_users[i]!=odebrana.sender){
                    i++;
                }
                
                
                int j=0;
                int t=1;
                while(t && j<3){
                    if(compare(odebrana.text, name_group[j]))
                        t=0;
                    j++;
                }
                if(group_users[j-1][i]){
                    strcpy(wyslana.text, "Usunieto z grupy\n");
                    group_users[j-1][i]=0;
                    printf("Usunieto %s z grupy %s\n", users_name[i], name_group[j-1]);
                }
                else{
                    strcpy(wyslana.text, "Nie byles/as w grupie\n");
                    printf("Niepowodzenie\n");
                }
                wyslana.type=odebrana.sender;
                msgsnd(mid, &wyslana, MEMORY, 0);
            }
            //////zablokuj//////
            if(odebrana.number==9){
            
                printf("Blokowanie\n");
                
                int index=-1;
                int i=0;
                while(pid_users[i]!=odebrana.sender){
                    i++;
                }
                int j=0;
                while(j<9){
                    if(compare(odebrana.text,users_name[j])){
                        index=j;
                        banned[i][index]=1;
                        strcpy(wyslana.text, "Zablokowano");
                        printf("UzyZablokowano %s przez %s\n", users_name[index], users_name[i]);
                        break;
                    }
                    j++;
                }
                j=0;
                if(index==-1){
                    
                    while(j<3){
                        
                        if(compare(odebrana.text,name_group[j])){
                        index=j;
                        banned[i][index+9]=1;
                        strcpy(wyslana.text, "Zablokowano");
                        printf("GroZablokowano %s przez %s\n", name_group[index], users_name[i]);
                        break;
                        }
                        j++;
                    }
                    
                }
                if(index==-1){
                    strcpy(wyslana.text, "Nie istnieje taki uzytkowni/grupa.\n");
                }
                wyslana.type=odebrana.sender;
                msgsnd(mid, &wyslana, MEMORY, 0);
            
                
            }
            ///odblokuj////
            if(odebrana.number==10){
                printf("Odblokowanie\n");
                int index=-1;
                int i=0;
                while(pid_users[i]!=odebrana.sender){
                    i++;
                }
                int j=0;
                while(j<9){
                    if(compare(odebrana.text,users_name[j])){
                        index=j;
                        banned[i][index]=0;
                        strcpy(wyslana.text, "Odblokowano");
                        break;
                    }
                    j++;
                }
                j=0;
                if(index==-1){
                    while(j<3){
                        if(compare(odebrana.text,name_group[j])){
                        index=j;
                        banned[i][index+9]=0;
                        strcpy(wyslana.text, "Odblokowano");
                        break;
                        }
                        j++;
                    }
                }
                if(index==-1){
                    strcpy(wyslana.text, "Nie istnieje taki uzytkowni/grupa.");
                }
                wyslana.type=odebrana.sender;
                msgsnd(mid, &wyslana, MEMORY, 0);
            }
        }
    }
    else{
        char sign;
        while(p){
            scanf("%c", &sign);
            if(sign=='q' || sign=='Q'){
                FILE *out=fopen("in.txt", "w");  
                for(int i=0;i<9;i++){
                    fprintf(out, "%s\n", users_name[i]);
                }
                for(int i=0;i<9;i++){
                    fprintf(out, "%s\n", users_password[i]);
                }
                for(int i=0;i<3;i++){
                    fprintf(out, "%s\n", name_group[i]);
                }
                for(int i=0;i<3;i++){  
                    for(int j=0;j<9;j++){
                        fprintf(out, "%d",group_users[i][j]);
                    }
                    fprintf(out, "\n");
                }
                printf("Zakonczenie programu\n\n");
                p=0;
                kill(serverPID, 9);
                fclose(out);
            }
        }
    }
}
