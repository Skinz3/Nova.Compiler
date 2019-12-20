#include "statement.h"
#include <vector>

using namespace std;

class MethodCallStatement : public Statement
{
public:
    MethodCallStatement(string line, string name,vector<Statement*>* parameters);
    static MethodCallStatement *Build(string line);

private:
    string name;
    vector<Statement*>* parameters;
};