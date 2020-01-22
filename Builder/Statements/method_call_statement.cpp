#include "method_call_statement.h"
#include <regex>
#include "../Utils/string_utils.h"
#include <iostream>
#include "statement_parser.h"

const std::regex METHOD_CALL_PATTERN{"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\\((.*)\\)$"};

MethodCallStatement::MethodCallStatement(string line, string name, vector<Statement *> *parameters) : Statement(line)
{
    this->name = name;
    this->parameters = parameters;
    cout << "[MethodCall Statement] " << line << endl;
}
MethodCallStatement *MethodCallStatement::Build(string line)
{
    smatch match;
    regex_search(line, match, METHOD_CALL_PATTERN);

    if (match.size() > 0)
    {
        string methodName = match[1];

        string parameters = StringUtils::Trim(match[2]);

        vector<string> splitted = StringUtils::Split(parameters,',');

        vector<Statement*>* params = new vector<Statement*>();

        for (string item : splitted)
        {
            Statement* s = StatementParser::ParseStatement(item);
            params->push_back(s);
        }

        return new MethodCallStatement(line, methodName, params);
    }
    else
    {
        return NULL;
    }
}
