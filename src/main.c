#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <../include/main.h>
#include <../src/parser.c>


nova_file openFile(char * filename) 
{
  FILE * fp;
  char * line = NULL;
  size_t len = 0;

  fp = fopen(filename, "r");

  if (fp == NULL)
    exit(EXIT_FAILURE);

  int lines = 0;
  while (!feof(fp)) 
  {
    char ch = fgetc(fp);
    if (ch == '\n') 
    {
      lines++;
    }
  }
  nova_file nova_file;

  nova_file.lines = malloc(sizeof(file_line) * lines);

  printf("Lines:%i\n", lines);

  int lineSize;
  int i = 0;
  while ((lineSize = getline( & line, & len, fp)) != -1) 
  {
    nova_file.lines[i].value = malloc(sizeof(char) * lineSize);
    strcpy(nova_file.lines[i].value, line);
    nova_file.lines[i].size = lineSize;
    i++;
  }

  fclose(fp);

  return nova_file;
}

int main(int argc, char ** argv) 
{
  char * fileName = "script.nv"; // argv[1];
  nova_file file = openFile(fileName);
  parseFile(file);
  return 0;
}