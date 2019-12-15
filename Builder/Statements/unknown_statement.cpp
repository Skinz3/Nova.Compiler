#include "unknown_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>

using namespace std;
 
UnknownStatement::UnknownStatement(string line) : Statement(line)
{
    cout << "Unknown Statement: " << line << endl;
};