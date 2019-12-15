#include "statement.h"

class AssignationStatement : public Statement
{
public:
    AssignationStatement(string line, string variableName, Statement *value);
    static AssignationStatement *Build(string line);

private:
    string variableName;
    Statement *value;
};