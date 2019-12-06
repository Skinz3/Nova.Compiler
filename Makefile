# TO BE COMPLETED
CFLAGS = -Wall -g -ggdb -I


compiler: compiler.o 
	gcc compiler.o -o compiler


compiler.o: ./include/main.h src/parser.c  src/main.c
	gcc $(CFLAGS) ./include/ -c src/main.c -o compiler.o


clean:
	rm *.o
