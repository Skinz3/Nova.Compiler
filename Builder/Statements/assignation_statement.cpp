#include "assignation_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const std::regex ASSIGNATION_PATTERN{"^([a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(\\+|-|\\*|\\/)?=\\s*(.+)$"};

AssignationStatement::AssignationStatement(string line, string variableName,char op, Statement *value) : Statement(line)
{
   this->variableName = variableName;
   this->value = value;
   this->op = op;
   cout << "[Assignation Statement] " << line << endl;
};
AssignationStatement *AssignationStatement::Build(string line)
{
   std::smatch match = StringUtils::Regex(line, ASSIGNATION_PATTERN);

   if (match.size() > 0)
   {
      string variableName = match[1];

      char op = match[2].str()[0];

      string value = match[3];

      Statement *st = StatementParser::ParseStatement(value);

      return new AssignationStatement(line, variableName, op, st);
   }
   else
   {
      return NULL;
   }
}