#include<stdio.h>

/*---------------------------------------------------------
 * lit sur l entree standard :
 *        un entier i
 *        une suite de caracteres de la forme .......%.......%........%
 * et recopie sur la sortie standard la sous-suite numero i
 * --------------------------------------------------------*/

int	main() {
  char	c;
  int	icherche,icour;

  scanf("%d",&icherche);
  icour=1;
  c = getchar();

  while((c != EOF) && (icour<=icherche)) {
  
    if (c=='%')
      icour ++;
    else if (icour==icherche)
      putchar(c);
  
    c = getchar();
  }
  printf("\n");
  return 0;
}
