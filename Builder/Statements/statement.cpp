#include "statement.h"
#include <string>

using namespace std;

Statement::Statement(string line)
{
    this->line = line;
}
bool Statement::ValidateSemantic()
{
    return true;
}
