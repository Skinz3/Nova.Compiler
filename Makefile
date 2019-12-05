# TO BE COMPLETED
CFLAGS = -Wall -g -ggdb 


main: main.o 
	gcc main.o -o main


main.o: main.c
	gcc $(CFLAGS) -c main.c -o main.o


clean:
	rm *.o
