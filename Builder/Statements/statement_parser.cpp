#include "statement_parser.h"
#include "assignation_statement.h"
#include "declaration_statement.h"

Statement *StatementParser::ParseStatement(string line)
{
    Statement *st;

    st = AssignationStatement::Build(line);

    if (st != NULL)
        return st;

    st = DeclarationStatement::Build(line);

    if (st != NULL)
        return st;

    return NULL;
}