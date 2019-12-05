/*  Read a number in seconds and it decomposes it in    */
/*  hours, minutes and seconds                          */

#include <stdio.h>

#define SECPARMN 60L                /* nb of sec in 1 mn  */
#define MNPARH   60                 /* nb of min in 1 h    */
#define HPARD    24                 /* nb of hours in a day */
#define SECPARH (SECPARMN * MNPARH)	/* nb of sec in an hour  */
#define SECPARD (SECPARH * HPARD)	/* nb of sec in a day   */


int main ()
{
  unsigned long int seconds, remaining;
  int days, hours, minutes;

  printf ("Nombre de seconds ? ");
  scanf ("%lu", &seconds);

  days = seconds / SECPARD;
  remaining = seconds % SECPARD;
  hours = remaining / SECPARH;
  remaining %= SECPARH;
  minutes = remaining / SECPARMN;
  remaining %= SECPARMN;

  printf ("%lu seconds are equal to %d days, %d hours, %d min and %lu sec\n",
	  seconds, days, hours, minutes, remaining);
}
