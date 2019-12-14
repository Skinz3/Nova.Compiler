#include "declaration_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>

const string DECLARARTION_PATTERN = "^([a-zA-Z_$][a-zA-Z_$0-9]*)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(=\\s*(.*))?$";

DeclarationStatement::DeclarationStatement(string line) : Statement(line)
{
 
};

DeclarationStatement *DeclarationStatement::Build(string line)
{
    std::smatch match = StringUtils::Regex(line, DECLARARTION_PATTERN);

    if (match.size() > 0)
    {
        string type = match[1];
        string variableName = match[2];
        string expressionValue = match[4]; // if == "" , no expression, declaration only.

        return new DeclarationStatement(line); // pass arguments
    }
    else
    {
        return NULL;
    }
    
}