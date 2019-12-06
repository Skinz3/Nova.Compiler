#include <stdio.h>
#include <stdlib.h>
#include <../include/expressions.h>


void parseFile(nova_file file)
{
    for (int i = 0;i< file.linesCount;i++)
    {
       char* value = file.lines[i].value;



        if (strstr(value, "=") != NULL) 
        {
            // assignation_expr exp;
            
            char * right = strtok(value, "=");
         

            printf("assignation_expr right:%s\n",right);
 
        }



       //printf("%s",value);
    } 
}
