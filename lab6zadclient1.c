#include <stdio.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>


char bufor[1024];
int main(int argc, char** argv){
    struct sockaddr_in saddr;
    char buffer[1024];
    char indeks[8] = "141284\n";
    struct hostent* addrent;
    int i=0;
    int written=0;
    addrent = gethostbyname(argv[1]);
    int fd = socket(AF_INET,SOCK_STREAM,0);
    
    saddr.sin_family=AF_INET;
    saddr.sin_port=htons(atoi(argv[2]));
    memcpy(&saddr.sin_addr.s_addr,addrent->h_addr,addrent->h_length);
    connect(fd,(struct sockaddr*)&saddr,sizeof(saddr));
    
    i = 0;
    while(i < 7){
        written = write(fd, indeks, 7);
        i=i+written;
    }
    i = 0;
    int rc=read(fd,buffer,255);
    i=i+rc;
    printf("%d \n",i);
    while(buffer[i-1]!='\n'){
            
        rc = read(fd,buffer+i,255);
        i=i+rc;
                  
    }
    write(1,buffer,i);
    close(fd);
    return EXIT_SUCCESS;
    
    
}
