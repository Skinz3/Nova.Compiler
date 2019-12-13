#include "assignation_statement.h"

AssignationStatement::AssignationStatement(string line) : Statement(line)
{

};
bool AssignationStatement::IsValid(string line)
{
    return true;
}