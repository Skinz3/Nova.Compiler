#include <stdio.h>

/** ---------------------------------------------------------
 * lit sur l'entrée standard une suite de caracteres terminee
 * par % et la recopie sur la sortie standard en doublant chaque
 * caractere
 * ---------------------------------------------------------*/

void main() {
  int c;
  do {
    c = getchar();
    putchar(c);
    putchar(c);
  } while(c!='%');
}

