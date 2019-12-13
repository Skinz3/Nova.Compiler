#include "statement.h"

class AssignationStatement : public Statement
{
public:
    AssignationStatement(string line);

    static AssignationStatement* Build(string line);
};