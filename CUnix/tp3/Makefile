CFLAGS=-g -ggdb -g3 -Wall

all: bug1 bug2 boucle perror prime liste1 liste2 boom


bug1: bug1.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

bug2: bug2.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

boucle: boucle.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

perror: perror.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

prime: prime.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

liste1: liste1.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

liste2: liste2.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

boom: boom.c
	$(CC) $(CFLAGS) -O0 -o $@ $<

clean:
	/bin/rm -f bug1 bug2 boucle perror prime liste1 liste2 boom
	/bin/rm -rf *.dSYM
