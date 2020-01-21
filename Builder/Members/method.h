#ifndef METHOD
#define METHOD

#include <string>
#include "enums.h"
#include <vector>
#include <map>
#include "../Statements/statement.h"
#include "../IO/Binary/binary_writer.cpp"

using namespace std;

class Method
{

public:
    Method(vector<string> *fileLines, map<int, int> *fileBrackets, int startIndex, int endIndex, string methodName, ModifierEnum modifier, string returnType, string parameters);
    bool Build();
    void Serialize(BinaryWriter* writer);
private:
    vector<string> *fileLines;
    map<int, int> *fileBrackets;

    vector<Statement*>* statements;

    int startIndex;
    int endIndex;

    string methodName;
    ModifierEnum modifier;
    string returnType;
    string parameters;
};

#endif // METHOD