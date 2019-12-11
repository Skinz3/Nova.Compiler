#include <string>

class Method
{

public:
    Method(std::string methodName,std::string modifier,std::string returnType,std::string parameters);

private:
    std::string methodName;
    std::string modifier;
    std::string returnType;
    std::string parameters;
};
