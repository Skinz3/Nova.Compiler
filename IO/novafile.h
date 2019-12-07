#include <string>
#include <vector>

using namespace std;

class NovaFile
{
public:
    NovaFile(string fileName);
    string fileName;
    
private:
    vector<string>* lines;
    void ReadLines();
};
