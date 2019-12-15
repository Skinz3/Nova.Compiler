#include "const_int_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const std::regex CONST_INT_PATTERN{"^([+-])?([0-9]{1,10})$"};

ConstIntStatement::ConstIntStatement(string line, int value) : Statement(line)
{
    this->value = value;
    cout << "[ConstantInt] " << value << endl;
};
ConstIntStatement *ConstIntStatement::Build(string line)
{
    std::smatch match = StringUtils::Regex(line, CONST_INT_PATTERN);

    if (match.size() > 0)
    {
        int value = std::stoi(match[2]);

        string sign = match[1];

        if (sign == "-")
        {
            value = -value;
        }

        if (value > 2147483647 || value < -2147483647) // this is  not possible. value is a int. think about it and fix it. (if value is 10 characters and > 2147483647 compiler will crash.)
        {
            return NULL;
        }

        return new ConstIntStatement(line, value);
    }
    else
    {
        return NULL;
    }
}