#include "statement.h"

class ConstIntStatement : public Statement
{
public:
    ConstIntStatement(string line, long long value);
    static ConstIntStatement *Build(string line);

private:
    int value;
};