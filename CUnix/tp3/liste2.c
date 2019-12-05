#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>


// definion of the element of the list.
typedef struct Cell{
  int el;
  struct Cell* nextC;
} Cell;

typedef Cell* Liste;

void insertHead(Liste* pL,int elem)
{
  //creation of element and its value
  Cell* pc = (Cell*) malloc(sizeof(Cell));
  (*pc).el = elem;
  (*pc).nextC = *pL;
  *pL = pc;
}

void freeList(List l)
{
  Cell* previous = NULL;
  Cell* current = l;

  while(current != NULL)
  {
    previous = *current;
    current = (*current).nextC;
    free(previous);
  }

}
void printListe(Liste l)
{
  Cell* pc = l;
  while(pc != NULL)
  {
    printf("%d,",(*pc).el);
    pc = (*pc).nextC ;
  }
  printf("\n");
}

int main(){
  Liste maliste;

  insertHead(&maliste,4);
  insertHead(&maliste,42);
  insertHead(&maliste,2);
  
  printListe(maliste);
	
  Cell* cell = getQueue(&maListe);
  return 0;
}
