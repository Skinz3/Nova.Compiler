#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>


// type definition
typedef struct Cell {
  int          key;   /* integer cell */
  struct Cell* nextC; /* ref to the next cell */
} Cell;

typedef Cell* Liste; // alias


// insert head
void insertinList(Liste* mylist, int el)
{
  Cell* newCell=(Cell*) malloc(sizeof(Cell));
  newCell->key = el;
  *mylist = newCell;
}


// print the list
void printList(Liste mylist)
{
  Cell* p = mylist;

  printf("[");
  while (p != NULL)
  {
    printf("%d", p->key);
    if (p->nextC != NULL)
    {
      printf(",");
    }
  }
  printf("]\n");
}


// creation
Liste creeListePositive()
{
  Liste thelist=NULL ;
  bool fini = false;
  int val;
  while (!fini)
  {
    printf("give an element >=0, and <0 if you want to stop\n");
    scanf("%d", &val);
    if (val>=0)
    {
      insertinList(&thelist, val);
    }
    else
    {
      fini = true;
    }
  }
  return thelist;
}


/********* Main function *********/
int main(int argc, char *argv[])
{
  // declaration
  Liste ltest1;

  // initialisation
  ltest1 = creeListePositive();

  // print
  printList(ltest1);
  return 0;
}
