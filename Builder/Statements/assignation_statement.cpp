#include "assignation_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const string ASSIGNATION_PATTERN = "^([a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(\\+|-|\\*|\\/)?=\\s*(.+)$"; // operators are handled here (+= -= *= /=)

AssignationStatement::AssignationStatement(string line,string variableName,Statement* value) : Statement(line)
{
   std::cout << "[Assignation Statement] " << line << std::endl;
};
AssignationStatement *AssignationStatement::Build(string line)
{
   std::smatch match = StringUtils::Regex(line, ASSIGNATION_PATTERN);

   if (match.size() > 0)
   {
      string variableName = match[1];
      string value = match[2];

      Statement *st = StatementParser::ParseStatement(value);

      return new AssignationStatement(line, variableName, st);
   }
   else
   {
      return NULL;
   }
}