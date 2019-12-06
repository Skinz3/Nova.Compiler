# TO BE COMPLETED
CFLAGS = -Wall -g -ggdb -I


compiler: compiler.o 
	gcc compiler.o -o compiler


compiler.o: ./include/main.h ./include/expressions.h src/output.cpp src/parser.cpp  src/main.cpp
	gcc $(CFLAGS) ./include/  -c src/main.cpp -o compiler.o


clean:
	rm *.o
