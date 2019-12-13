#include <string>
#include "method.h"
#include "iostream"
#include "../Utils/string_utils.h"
#include "../Statements/statement_parser.h"

using namespace std;

Method::Method(vector<string> *fileLines, map<int, int> *fileBrackets, int startIndex, int endIndex, string methodName, ModifierEnum modifier, std::string returnType, std::string parameters)
{
    this->fileLines = fileLines;
    this->fileBrackets = fileBrackets;
    this->startIndex = startIndex;
    this->endIndex = endIndex;
    this->methodName = methodName;
    this->modifier = modifier;
    this->parameters = parameters;
    this->returnType = returnType;
}

bool Method::Build()
{
    this->statements = new vector<Statement*>();

    for (int i = startIndex; i < endIndex; i++)
    {
        if (!StringUtils::IsEmptyOrWhiteSpace(fileLines->at(i)))
        {
            string line = StringUtils::Trim(fileLines->at(i));

            Statement* statement = StatementParser::ParseStatement(line);
            
            cout << "we need to parse:" << line << endl;
        }
    }

    return true;
}