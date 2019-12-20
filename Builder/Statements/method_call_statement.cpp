#include "method_call_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>

const std::regex METHOD_CALL_PATTERN{"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\\((.*)\\)$"};

MethodCallStatement::MethodCallStatement(string line, string name, vector<Statement *> *parameters) : Statement(line)
{
    this->name = name;
    this->parameters = parameters;
    cout << "[MethodCall Statement] " << line << endl;
}
MethodCallStatement *MethodCallStatement::Build(string line)
{
    std::smatch match = StringUtils::Regex(line, METHOD_CALL_PATTERN);

    if (match.size() > 0)
    {
        return new MethodCallStatement(line, "", NULL);
    }
    else
    {
        return NULL;
    }
}