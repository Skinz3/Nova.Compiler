#include <vector>
#include "method.h"


using namespace std;

class Class
{

public:
    Class(vector<string> lines);

private:
    vector<Method>* methods;
};
