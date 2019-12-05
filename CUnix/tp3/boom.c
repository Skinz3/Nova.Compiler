#include <stdio.h>
#include <stdlib.h>

int main(int argc, char** argv)
{
  int *p;

  printf("1");
  p = NULL;

  printf("2");
  *p = 1;

  printf("3");
  return 0;
}
