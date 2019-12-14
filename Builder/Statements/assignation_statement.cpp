#include "assignation_statement.h"
#include <regex>
#include "../Utils/string_utils.h"

const string ASSIGNATION_PATTERN = "";

AssignationStatement::AssignationStatement(string line) : Statement(line)
{

};
AssignationStatement* AssignationStatement::Build(string line)
{
   return NULL;
    
}