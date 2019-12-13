#include "declaration_statement.h"
#include <regex>

const string DECLARARTION_PATTERN = "(?<typename>[a-zA-Z_$][a-zA-Z_$0-9]*)\\s+(?<variable>[a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(?<initializerclause>=\\s*(?<initializer>.*))?";

DeclarationStatement::DeclarationStatement(string line) : Statement(line)
{

};
DeclarationStatement* DeclarationStatement::Build(string line)
{
    return NULL;
}