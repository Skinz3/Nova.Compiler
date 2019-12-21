#include <string>
#include <vector>
#include "../Members/class.h"

using namespace std;

class Assembly
{
public:
    Assembly(string name, vector<Class *> *classes);

private:
    vector<Class*>* classes;
    string name;
};