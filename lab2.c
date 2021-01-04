#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>





int main(int argc, char** argv){
    struct sockaddr_in s; 
    struct hostent* addrent;
    char buffer[1024];
    char mess[1024] = { ' ' };
    addrent = gethostbyname(argv[1]);
    
    int decision;
    
    int fd = socket(AF_INET, SOCK_STREAM,0);
    s.sin_family = AF_INET;
    s.sin_port = htons(atoi(argv[2]));
    memcpy(&s.sin_addr.s_addr, addrent->h_addr, addrent->h_length);
    strcpy(buffer, " ");
    connect(fd, (struct sockaddr*)&s, sizeof(s));
    
    write(fd, "1\njeden\n", 8);
    
    decision =1;
    
    if( decision==1 ){
        printf("\n Dolacz\n");
        write(fd, "1\n1\nA", 5);
        int newline=0;
        int i=0;
        
        //historia
        while(newline!=1){
            strcpy(buffer, " ");
            read(fd, buffer, 1);
            //printf("-> %c\n", buffer[0]);
            printf(">>> %c\n", buffer[0]);
            mess[i] = buffer[0];
            
            if(mess[i-1] == '\n' && mess[i] == '\n'){
                newline=1;
            }
            i++;
        }
        
        printf("----> %s\n", mess);
        strcpy(buffer, " ");
    }
    decision=2;
    
        memset(mess,0, strlen(mess));
        strcpy(buffer, " ");
    if(decision==2){
        printf("\n Wyslij wiadomosc\n");
        int newline=0;
        int i=0;
        write(fd, "2\n10\nwiadomommx", 15);
        while(newline!=1){
            
            read(fd, buffer, 1);
            //printf("-> %c\n", buffer[0]);
            printf(">>> %c\n", buffer[0]);
            mess[i] = buffer[0];
            
            if(mess[i] == '\n'){
                newline=1;
            }
            i++;
        }
        
        printf("----> %s\n", mess);
        printf("----> \n");
        
    }
    
    //decision =3;
    if(decision==3){
        printf("wyloguj\n");
        write(fd, "3\n0\n", 4);
        
    }
    // decision=5;
    if(decision==5){
        printf("usun\n");
        write(fd, "5\n1\nC", 5);
        printf("usun\n");
     
    }
    //decision=4;
    if(decision==4){
        printf("nowy\n");
        write(fd, "4\n1\nb", 5);
        
    }
    
    decision=6;
    if(decision==6){
        int i=0;
        int newline=0;
        printf("do uzytkownika\n");
        write(fd, "6\n7\n1\thejka",11);
        while(newline!=1){
            
            read(fd, buffer, 1);
            //printf("-> %c\n", buffer[0]);
            printf(">>> %c\n", buffer[0]);
            mess[i] = buffer[0];
            
            if(mess[i] == '\n'){
                newline=1;
            }
            i++;
        }
        printf("mess: %s", mess);
        
    }
    
    decision=7;
    if(decision==7){
        printf("wyswietl forum\n");
        write(fd, "7\n0\n", 4);
        int newline =0;
        int i=0;
        while(newline!=1){
            
            read(fd, buffer, 1);
            //printf("-> %c\n", buffer[0]);
            printf(">>> %c\n", buffer[0]);
            mess[i] = buffer[0];
            
            if(mess[i] == '\n'){
                newline=1;
            }
            i++;
        }
        printf("lista: %s\n", mess);
        
        
    }
   decision=8;
    if(decision==8){
        printf("wyswietl uzytkownikow\n");
        write(fd, "8\n0\n", 4);
        int newline =0;
        int i=0;
        while(newline!=1){
            
            read(fd, buffer, 1);
            //printf("-> %c\n", buffer[0]);
            printf(">>> %c\n", buffer[0]);
            mess[i] = buffer[0];
            
            if(mess[i] == '\n'){
                newline=1;
            }
            i++;
        }
        printf("lista: %s\n", mess);
        
        
    }
    close(fd);
    return EXIT_SUCCESS;
}
