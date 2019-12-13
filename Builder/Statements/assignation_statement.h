#include "statement.h"

class AssignationStatement : public Statement
{
public:
    AssignationStatement(string line);

    static bool IsValid(string line);
};