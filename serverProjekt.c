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
void* cthread(void* arg){
    char buffer[1024];
    int counter = 0;
    int countNewLine = 0;
    int logged;
    char flag;
    int size=0;
    int i=0;
    logged = 1;
    //funkcja logowania
    struct cln* c = (struct cln*)arg;
    
    while(logged){
        while(countNewLine <2){
            read(c->cfd, buffer, 1);
             printf("tekst: %s\n", buffer);
            if( countNewLine == 0){
                flag = buffer[0];
            }
            if (countNewLine != 0 && buffer[0] != '\n'){
                size = size * 10 + buffer[0] - '0';
            }
            if ( buffer[0] == '\n'){
                countNewLine++;
            }
            
        }
         printf("size %d\n", size);
        countNewLine=0;
        while(i < size){
        
            counter = read(c->cfd, buffer+i, 1024);
            printf("teksttt: %s %d %d %d\n", buffer, counter, i, size);
            i+= counter;
        }
        i=0;
        printf("tekst: %s\n", buffer);
        logged = 0;
    }
    close(c->cfd);
    free(c);
    return EXIT_SUCCESS;
}
int main(int argc, char** argv){
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
    
    
