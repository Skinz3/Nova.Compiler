#include "declaration_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const string DECLARARTION_PATTERN = "^([a-zA-Z_$][a-zA-Z_$0-9]*)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(=\\s*(.*))?$";

DeclarationStatement::DeclarationStatement(string line, string type, string variableName, Statement *value) : Statement(line)
{
    this->type = type;
    this->variableName = variableName;
    this->value = value;
};

DeclarationStatement *DeclarationStatement::Build(string line)
{
    std::smatch match = StringUtils::Regex(line, DECLARARTION_PATTERN);

    if (match.size() > 0)
    {
        string type = match[1];
        string variableName = match[2];
        string statementValue = match[4]; 

        Statement*  st = StatementParser::ParseStatement(statementValue);

        return new DeclarationStatement(line, type, variableName, st); 
    }
    else
    {
        return NULL;
    }
}