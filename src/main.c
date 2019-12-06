#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <../include/main.h>
#include <../src/parser.c>

int countLines(FILE* fp) 
{
  int lines = 0;
  while (!feof(fp)) 
  {
    char ch = fgetc(fp);
    if (ch == '\n') 
    {
      lines++;
    }
  }
  fseek(fp, 0, SEEK_SET); // we put reader position to 0 to read file lines.
  
  return lines+1;
}

/*
 This function open stream with file, read lines count and store all lines 
 in nova_file.lines. FILE is then disposed.
*/
nova_file openFile(char * filename) 
{
  FILE * fp;
  char * line = NULL;
  size_t len = 0;
  int lineSize = 0;
  int i = 0;

  fp = fopen(filename, "r");

  if (fp == NULL)
    exit(EXIT_FAILURE); // Unable to read file.

 
  nova_file nova_file;
  nova_file.fileName = filename;
  nova_file.linesCount = countLines(fp);
 
  nova_file.lines = malloc(sizeof(file_line) * nova_file.linesCount);

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