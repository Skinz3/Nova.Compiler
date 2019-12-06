#include <stdio.h>
#include <stdlib.h>
#include <../include/expressions.h>


void parseFile(nova_file file)
{
    for (int i = 0;i< file.linesCount;i++)
    {
       char* value = file.lines[i].value;
       printf("%s",value);
    } 
}
