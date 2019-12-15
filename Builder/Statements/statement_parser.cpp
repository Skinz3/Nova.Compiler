#include "statement_parser.h"
#include "../Utils/string_utils.h"
#include "assignation_statement.h"
#include "declaration_statement.h"
#include "const_int_statement.h"
#include "empty_statement.h"
#include "unknown_statement.h"

/*
    Not very smart, but probably the fastest way
    (instead of map<regex,type> or reflection)
*/
Statement *StatementParser::ParseStatement(string rawLine)
{
    string line = StringUtils::Trim(rawLine);

    if (line == "")
    {
        return new EmptyStatement(line);
    }

    Statement *st;

    st = AssignationStatement::Build(line);

    if (st != NULL)
        return st;

    st = DeclarationStatement::Build(line);

    if (st != NULL)
        return st;

    st = ConstIntStatement::Build(line);

    if (st != NULL)
        return st;
        
    return new UnknownStatement(line);
}