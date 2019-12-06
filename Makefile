# TO BE COMPLETED
CFLAGS = -Wall -g -ggdb -I


main: main.o 
	gcc main.o -o main


main.o: ./include/main.h src/parser.c  src/main.c
	gcc $(CFLAGS) ./include/ -c src/main.c -o main.o


clean:
	rm *.o
