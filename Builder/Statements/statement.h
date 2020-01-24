#ifndef STATEMENT
#define STATEMENT

#include <string>

using namespace std;

class Statement
{
public:
    Statement(string line);

    virtual bool ValidateSemantic();

private:
    string line;
};

#endif // STATEMENT