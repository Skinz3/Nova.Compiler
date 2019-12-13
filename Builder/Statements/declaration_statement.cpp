#include "declaration_statement.h"

const string DECLARARTION_PATTERN = "";

DeclarationStatement::DeclarationStatement(string line) : Statement(line)
{

};
bool DeclarationStatement::IsValid(string line)
{
    return false;
}