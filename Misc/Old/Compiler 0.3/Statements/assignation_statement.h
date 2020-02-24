#include "statement.h"

class AssignationStatement : public virtual Statement
{
public:
    AssignationStatement(string line, string variableName,char op, Statement *value);
    static AssignationStatement *Build(string line);
    bool ValidateSemantic();

private:
    string variableName;
    Statement *value;
    char op;
};