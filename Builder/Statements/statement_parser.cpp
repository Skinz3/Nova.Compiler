#include "statement_parser.h"
#include "assignation_statement.h"
#include "declaration_statement.h"

Statement* StatementParser::ParseStatement(string line)
{
    if (AssignationStatement::IsValid(line))
    {
        return new AssignationStatement(line);
    }
    else if (DeclarationStatement::IsValid(line))
    {
        return new DeclarationStatement(line);
    }

    return NULL;
}