#ifndef METHOD
#define METHOD

#include <string>
#include "enums.h"
#include <vector>
#include <map>

using namespace std;

class Method
{

public:
    Method(vector<string> *fileLines, map<int, int> *fileBrackets, int startIndex, int endIndex, string methodName, ModifierEnum modifier, string returnType, string parameters);
    bool Build();
private:
    vector<string> *fileLines;
    map<int, int> *fileBrackets;

    int startIndex;
    int endIndex;

    string methodName;
    ModifierEnum modifier;
    string returnType;
    string parameters;
};

#endif // METHOD