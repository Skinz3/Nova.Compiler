#include <iostream>
#include <vector>
#include <string>
//#include "../IO/novafile.h"

using namespace std;

class Builder
{
public:
    static bool Build(vector<NovaFile*> files, string assemblyName);
}; 