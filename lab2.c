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
    
    int fd = socket(AF_INET, SOCK_STREAM,0);
    s.sin_family = AF_INET;
    s.sin_port = htons(atoi(argv[2]));
    memcpy(&s.sin_addr.s_addr, addrent->h_addr, addrent->h_length);
    strcpy(buffer, " ");
    connect(fd, (struct sockaddr*)&s, sizeof(s));
    
    write(fd, "1\njeden\n", 8);
    
    write(fd, "1\n1\nA", 6);
    int newline=0;
    int i=0, counter=0;
    
    //historia
    while(newline!=1){
        strcpy(buffer, " ");
        counter=read(fd, buffer, 1);
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
    write(fd, "1\n1\nA", 6);
    
    newline=0;
    i=0; counter=0;
    
    //historia
    while(newline!=1){
        strcpy(buffer, " ");
        counter=read(fd, buffer, 1);
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
    close(fd);
    return EXIT_SUCCESS;
}
