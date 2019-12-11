#include <string>
#include "method.h"



Method::Method(std::string methodName,std::string modifier,std::string returnType,std::string parameters)
{
    this->methodName= methodName;
    this->modifier = modifier;
    this->parameters = parameters;
    this->returnType = returnType;
}

