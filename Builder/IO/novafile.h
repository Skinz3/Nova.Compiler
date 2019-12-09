#include <string>
#include <vector>
#include <map>

using namespace std;

struct FileDefinition
{
    string className;
    string classNamespace;
    vector<string> classImports;
};

class NovaFile
{

public:
    NovaFile(string fileName);
    string fileName;
    void Print();
    bool Read();

private:
    FileDefinition definition;
    vector<string> *lines;
    bool ReadLines();
    string Search(string pattern, int index);
};
