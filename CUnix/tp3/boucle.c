#include <stdio.h>

int main()
{
  int i,j,k;
  int counter = 0;

  for(i=0; i < 20; i++)
  {
      for(j=10; j > 0; j--)
      {
          for(k=0; k < j; k--){counter++;}
      }
  }

  printf("counter = %d\n", counter);
  return 0;
}
