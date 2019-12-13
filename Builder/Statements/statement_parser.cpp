#include "statement_parser.h"
#include "assignation_statement.h"

Statement* StatementParser::ParseStatement(string line)
{
    if (AssignationStatement::IsValid(line))
    {
        return new AssignationStatement(line);
    }

    return NULL;
}