#include "statement.h"

class DeclarationStatement : public Statement
{
public:
    DeclarationStatement(string line);

    static bool IsValid(string line);
};