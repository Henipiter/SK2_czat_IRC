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


struct msgbuf{
    long type;
    char text[1024]; //tresc wlasciwa
    char text2[1024]; //pomocnicza np nazwa grupy/uzytkownika
    int number;
    long sender;
};
struct msgbuf odebrana;
struct msgbuf wyslana;
    
int MEMORY=2048+sizeof(long)*2+sizeof(int)+1;

int try;

char vchar(){
    char cc;
    while((cc=getchar())<=' ');
    return cc;
}

char zalog(int mid){
    char uzyt[16];
    char haslo[256];
    printf("Podaj nazwe uzytkownika (1-9)\n");
    scanf("%s", uzyt);
    printf("Podaj haslo\n");
    scanf("%s", haslo);
    wyslana.type=10;
    wyslana.number=1;
    wyslana.sender=getpid();
    strcpy(wyslana.text,uzyt);
    strcpy(wyslana.text2,haslo);
    msgsnd(mid, &wyslana,MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    try=odebrana.number;
    return odebrana.text[0];
}    
void wylog(int mid){
    wyslana.sender=getpid();
    wyslana.type=10;
    wyslana.number=2;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void do_uzyt(int mid){
    getchar();
    printf("Podaj nazwe uzytkownika\n");
    fgets(wyslana.text2,1024,stdin);
    
    strtok(wyslana.text2, "\n");
    printf("Podaj wiadomosc\n");
    fgets(wyslana.text,1024,stdin);
    wyslana.sender=getpid();
    wyslana.type=10;
    wyslana.number=5;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s\n",odebrana.text);
}
void do_grup(int mid){
    getchar();
    printf("Podaj nazwe grupy\n");
    fgets(wyslana.text2,1024,stdin);
    strtok(wyslana.text2, "\n");
    printf("Podaj wiadomosc\n");
    fgets(wyslana.text,1024,stdin);
    strtok(wyslana.text, "\n");
    wyslana.sender=getpid();
    wyslana.type=10;
    wyslana.number=6;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void dodaj_grup(int mid){
    printf("Podaj nazwe grupy");
    getchar();
    fgets(wyslana.text,1024,stdin);
    
    strtok(wyslana.text, "\n");
    wyslana.sender=getpid();
    wyslana.type=10;
    wyslana.number=7;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void usun_grup(int mid){
    getchar();
    printf("Podaj nazwe grupy");
    
    
    fgets(wyslana.text,1024,stdin);
    strtok(wyslana.text, "\n");
    wyslana.sender=getpid();
    wyslana.type=10;
    wyslana.number=8;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void zablok(int mid){
    getchar();
    printf("Podaj nazwe uzytkownia lub grupy\n");
    fgets(wyslana.text,1024,stdin);
    
    strtok(wyslana.text, "\n");
    wyslana.type=10;
    wyslana.number=9;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void odblok(int mid){
    getchar();
    printf("Podaj nazwe uzytkownia lub grupy\n");
    fgets(wyslana.text,1024,stdin);
    
    strtok(wyslana.text, "\n");
    wyslana.type=10;
    wyslana.number=10;
    msgsnd(mid, &wyslana, MEMORY, 0);
    msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
    printf("%s",odebrana.text);
}
void wysw_grup(int mid){
    wyslana.type=10;
    wyslana.number=4;
    wyslana.sender=getpid();
    strcpy(wyslana.text, " "); 
    msgsnd(mid, &wyslana, MEMORY, 0);
    printf("Sklad grup:\n");
    for(int i=0;i<3;i++){
        msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
        printf("%s\n", odebrana.text);
        for(int j=0;j<9;j++){
            msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
            
            if(odebrana.number){
                printf("-%s\n", odebrana.text);
            }
        }
    }
}
void wysw_uzyt(int mid){
    wyslana.type=10;
    wyslana.number=3;
    wyslana.sender=getpid();
    strcpy(wyslana.text, " "); 
    msgsnd(mid, &wyslana, MEMORY, 0);

    printf("Zalogowani uzytkownicy:\n");
    for(int i=0;i<9;i++){
        msgrcv(mid, &odebrana, MEMORY, getpid(), 0);
        
        if(odebrana.number){
            printf("%s\n", odebrana.text);
        }
    }
    
}
    
    
int main(int args, char* argv[]){
    char wybor,wybor2;
    int MEMORY=2048+sizeof(long)*2+sizeof(int)+1;
    int back;
    int mid=msgget(0x123, 0600 | IPC_CREAT);
    int mid2=msgget(0x124, 0600 | IPC_CREAT);
    int o=1, p=1;
    while(o){
        printf("\nWybierz: \n1.Zaloguj\n2.Zamkniecie programu\n");
        wybor=vchar();
        switch(wybor){
            case '1':
                back=zalog(mid);
                if(back=='q'){
                    printf("Zablokowano konto\n");
                }
                else if(back=='1'){
                    printf("Zalogowano\n");
                    p=1;
                    while(p){
                        printf("\n");
                        printf("Wybierz: \n1.Odbierz wiadomosc \n2.Lista zalogowanych \n3.Wyslij wiad do uzyt \n4.Wyslij do grupy \n5.Zapisz sie do grupy \n6.Wypisz sie z grupy  \n7.Wyloguj\n8.Zablokuj\n9.Odblokuj\na.Podglad grup\n");
                        wybor2=vchar();
                        switch(wybor2){
                            case '1':
                                printf("Odbieranie wiadomosci\n");
                                if(msgrcv(mid2, &odebrana, MEMORY, getpid(), IPC_NOWAIT)==-1){
                                    printf("Brak wiadomości");
                                }
                                else{
                                printf("Wiadomosc od %s.\n%s", odebrana.text2, odebrana.text);
                                }
                                
                                break;
                            case '2':
                                wysw_uzyt(mid);
                                break;
                            case '3':
                                do_uzyt(mid);
                                break;
                            case '4':
                                do_grup(mid);
                                break;
                            case '5':
                                dodaj_grup(mid);
                                break;
                            case '6':
                                usun_grup(mid);
                                break;
                            case '7':
                                wylog(mid);
                                p=0;
                                break;
                            case '8':
                                zablok(mid);
                                break;
                                
                            case '9':
                                odblok(mid);
                                break;
                            case 'a':
                                wysw_grup(mid);
                                break;
                        }
                    }
                }
                else if(back=='3'){
                    printf("Bledny login.\n");
                }
                else if(back=='4'){
                    printf("Uzytkownik jest juz zalogowany\n");
                }
                else{
                     printf("Bledny login lub haslo. Pozostalo %d prob.\n", try);
                }
                break;
            case '2':
                printf("Zakonczono dzialanie programu");
                o=0;
                break;
        };
    }
}
