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
    addrent = gethostbyname(argv[1]);
    int rc;
    int fd = socket(AF_INET, SOCK_STREAM,0);
    s.sin_family = AF_INET;
    s.sin_port = htons(atoi(argv[2]));
    memcpy(&s.sin_addr.s_addr, addrent->h_addr, addrent->h_length);
    
    connect(fd, (struct sockaddr*)&s, sizeof(s));
    write(fd, "1\n4\nsupp", 15);
    
    close(fd);
    return EXIT_SUCCESS;
}
