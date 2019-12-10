
#include <string>

using namespace std;

class Method
{

public:
    Method(string methodName,Modifier modifier,string returnType,string parameters);

private:
    string methodName;
    Modifier modifier;
    string returnType;
    string parameters;
};
