#include <string>
#include "enums.h"


class Method
{

public:
    Method(std::string methodName,ModifierEnum modifier,std::string returnType,std::string parameters);

private:
    std::string methodName;
    ModifierEnum modifier;
    std::string returnType;
    std::string parameters;
};
