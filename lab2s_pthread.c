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
    char buffer[7];
    char indeks[7] = "141284";
    char nazwisko[64] = "Milewski";
    struct cln* c = (struct cln*)arg;
    read(c->cfd, buffer, 7);
            if( strcmp(indeks, buffer) == 0){
                write(c->cfd, nazwisko, 64);
            }
            else{
                write(c->cfd, "ERROR", 5);
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
    
    
