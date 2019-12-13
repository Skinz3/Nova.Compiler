#include "statement.h"

class DeclarationStatement : public Statement
{
public:
    DeclarationStatement(string line);

    static DeclarationStatement* Build(string line);
};