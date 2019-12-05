#include <stdio.h>
#include <stdlib.h>

#define SIZE 1337

int main (void)
{
    int *tab;
    int i;

    tab = malloc (SIZE * sizeof(int));

    if ((tab = NULL))
    {
        perror ("malloc()");
        exit (-1);
    }

    for (i = 0; i < SIZE; i++)
    {
        tab[i] = random();
    }

    return 0;
}
