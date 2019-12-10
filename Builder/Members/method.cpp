#include <string>
#include "method.h"

using namespace std;

Method::Method(string methodName,Modifier modifier,string returnType,string parameters)
{
    this->methodName= methodName;
    this->modifier = modifier;
    this->parameters = parameters;
    this->returnType = returnType;
}

