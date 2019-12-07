#include <string>
#include <vector>
#include <map>

using namespace std;

class Expression
{
public:
    Expression(string line);
    string fileName;
    
private:
    vector<string>* lines;
    void ReadLines();
};
