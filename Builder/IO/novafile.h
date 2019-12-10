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
    bool ReadBrackets();
    vector<string> FindLinesUnderIndent(int startLineIndex, int minIndent);
    int GetIndentLevel(int lineIndex);
    string SearchFirst(string pattern, int index);
    vector<string> Search(string pattern, int index);
    map<int,int> brackets;

    vector<Class> *classes;
};
