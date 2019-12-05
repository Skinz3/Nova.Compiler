CFLAGS = -Wall -g -ggdb -I


mylist: test_list.o list.o
	gcc test_list.o list.o -o mylist 


list.o:./include/list.h ./src/list.c
	gcc $(CFLAGS) ./include -c ./src/list.c -o list.o


test_list.o:./include/list.h ./src/test_list.c
	gcc $(CFLAGS) ./include -c ./src/test_list.c -o test_list.o

clean:
	rm *.o
