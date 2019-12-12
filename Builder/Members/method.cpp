#include <string>
#include "method.h"

using namespace std;

Method::Method(vector<string> *fileLines, map<int, int> *fileBrackets,int startIndex,int endIndex,string methodName,ModifierEnum modifier,std::string returnType,std::string parameters)
{
    this->fileLines = fileLines;
    this->fileBrackets = fileBrackets;
    this->startIndex = startIndex;
    this->endIndex = endIndex;
    this->methodName= methodName;
    this->modifier = modifier;
    this->parameters = parameters;
    this->returnType = returnType;
}

bool Method::Build()
{
    return true;
}