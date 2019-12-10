#include <string>
#include <vector>
#include <map>
#include "../Members/class.h"
#include <regex>

using namespace std;

struct FileDefinition
{
    string _namespace;
    vector<string> usings;
};

class NovaFile
{

public:
    NovaFile(string fileName);
    string fileName;
    void Print();
    bool Read();
    bool ReadClasses();

private:
    FileDefinition definition;
    vector<string> *lines;
    bool ReadLines();
    string SearchFirst(string pattern, int index);
    vector<string> Search(string pattern,int index);
    
    vector<Class>* classes;
};
