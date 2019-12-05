#
# Link
#

bin/test_list: ./obj/test_list.o ./obj/list.o
	gcc $(CFLAGS) -o bin/test obj/test_list.o obj/list.o
# To be completed

#
# Objets of TP Lists
#

obj/list.o: ./include/list.h ./src/list.c
	gcc $(CFLAGS) -I ./include -c ./src/list.c 
# To be completed
obj/test_list.o: ./src/test_list.c
	gcc $(CFLAGS) -I include -c src/test_list.c
# To be completed

#
# Listing creation in pdf
#

listing:
	
	ps2pdf listing.ps
	rm listing.ps
# To be completed

#
# Remove files
#

reset : 
	make clean
	

clean :
# To be completed
	

#boubu
# Test the functionality 
#

test :
# To be completed
