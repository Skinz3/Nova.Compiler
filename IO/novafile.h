#include <string>
#include <vector>
#include <map>

using namespace std;

struct FileDefinition
{
    string className;
    vector<string> *imports;
};

class NovaFile
{
    public:

    NovaFile(string fileName);
    string fileName;

    private:
    
    FileDefinition definition;
    vector<string> *lines;
    map<int, int> brackets;
    void Read();
    void ReadLines();
    void ReadDefinition();
};
