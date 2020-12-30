#include <stdio.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>
#include <signal.h>
#include <sys/wait.h>

int main(int argc, char** argv){
    socklen_t slt;
    int sfd,cfd,on=1;
    struct sockaddr_in saddr,caddr;
    char buffer[256];
    int i=0;
    int written=0;
    sfd = socket(AF_INET,SOCK_STREAM,0);
    saddr.sin_family = AF_INET;
    saddr.sin_addr.s_addr=INADDR_ANY;
    saddr.sin_port = htons(1234);
    setsockopt(sfd,SOL_SOCKET,SO_REUSEADDR,(char*)&on,sizeof(on));
    bind(sfd,(struct sockaddr*)&saddr,sizeof(saddr));
    listen(sfd,5);
    while(1){
        i=0;
        slt=sizeof(caddr);
        cfd=accept(sfd,(struct sockaddr*)&caddr,&slt);
        printf("new connection: %s\n",inet_ntoa((struct in_addr)caddr.sin_addr));
        int rc=read(cfd,buffer,255);
        i=i+rc;
        printf("%d \n",i);
        while(buffer[i-1]!='\n'){
            
            rc = read(cfd,buffer+i,255);
            i=i+rc;
                  
            }
            
        if(strcmp(buffer,"141284\n")==0){
            int i = 0;
            while(i < 17){
                written = write(cfd, "Cezary Milewski\n", 16);
                i=i+written;
            }
        }else{
            
            int i = 0;
            while(i < 7){
                written = write(cfd, "ERROR!\n", 7);
                i=i+written;
            }
        }
        close(cfd);
    }
    close(sfd);
    return EXIT_SUCCESS;
}
