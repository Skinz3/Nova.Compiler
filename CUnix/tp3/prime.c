/**************************************
 * Sieve of Eratosthenes
 *
 * The Sieve of Eratosthenes is a procedure  
 * that allows to find the prime number
 * up to a given limit
 *
 **************************************/

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#define MAX 100

int prime[MAX];
int Ubound;

void checkPrime(int val)
{
  int i=2;
  while (1)
  {
    if (prime[i] == 1)
    {
      if (val % i == 0) /* i dividez val so val it is not a prime */
      {
          prime[val] = 0;
          return;
      }
    }
    i++;
  }

  /* val is prime */
  prime[val] = 1;
}

int main(int argc, char* argv[])
{
    int n;

    if (argc != 2)
    {
        fprintf(stderr, "usage: %s  Ubound\n", argv[0]);
        exit(EXIT_FAILURE);
    }
    
    Ubound = atoi(argv[1]);
    if(Ubound <0 || Ubound >= MAX)
    {
        fprintf(stderr, "usage: 2 <= Ubound <= %d\n",MAX);
        exit(EXIT_FAILURE);
    }

    prime[2] = 1;
    for (n = 3; n <= Ubound; n += 2)
    {
    	checkPrime(n);
    	if (prime[n])
    	{
        	printf("%d est prime\n", n);
    	}
    }

  return 0;
}
