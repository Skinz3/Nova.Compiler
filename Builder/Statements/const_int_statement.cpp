#include "const_int_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const std::regex CONST_INT_PATTERN{"^([+-])?([0-9]+)$"};

ConstIntStatement::ConstIntStatement(string line, long long value) : Statement(line)
{
    this->value = value;
    cout << "[ConstInt Statement] " << value << endl;
};
ConstIntStatement *ConstIntStatement::Build(string line)
{
    std::smatch match = StringUtils::Regex(line, CONST_INT_PATTERN);

    if (match.size() > 0)
    {
        try
        {
            long long value = std::stoll(line);
            return new ConstIntStatement(line, value);
        }
        catch (out_of_range)
        {
            cout << "Invalid Integer64 (out of range): " << line << endl;
            return NULL;
        }
    }
    else
    {
        return NULL;
    }
}