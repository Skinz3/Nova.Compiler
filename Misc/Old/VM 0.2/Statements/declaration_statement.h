#include "statement.h"

class DeclarationStatement : public Statement
{
public:
    DeclarationStatement(string line,string type,string variableName,Statement* value);

    static DeclarationStatement *Build(string line);

private:
    string type;
    string variableName;
    Statement* value;
};