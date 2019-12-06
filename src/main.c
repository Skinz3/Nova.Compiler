#include <stdio.h>
#include <stdlib.h>
#include <../include/main.h>

const int MAX_LINES_IN_FILE = 500;

nova_file openFile(char* filename) 
{
    nova_file nova_file;

    file_line * lines = malloc(sizeof(file_line) * MAX_LINES_IN_FILE);
    
    FILE * fp;
    char * line = NULL;
    size_t len = 0;
    int read;

    fp = fopen(filename, "r");

    if (fp == NULL)
        exit(EXIT_FAILURE);

    int i = 0;
    while ((read = getline(&line, &len, fp)) != -1) 
    {
        lines[i].value = line;
        lines[i].size = read;
        i++;
    }

    fclose(fp);

    nova_file.lines = lines;

    return nova_file;
}

int main(int argc, char **argv)
{
    char* fileName = "script.nv"; // argv[1];

    nova_file file = openFile(fileName);
    char * line =  file.lines[3].value;

    printf("line: %s\n",line);
  
   
    return 0;
}

 

