
typedef struct file_line file_line;

struct file_line 
{
	char* value;
	int size;
};


typedef struct nova_file nova_file;
struct nova_file
{
	char* fileName;
	file_line* lines;
	int linesCount;
};
