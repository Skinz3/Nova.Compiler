#ifndef BUILDER
#define BUILDER

#include <iostream>
#include <vector>
#include <string>
#include <map>
#include "../IO/nvfile.h"
#include "../Members/class.h"

using namespace std;

class Builder
{
public:
    static bool Build(vector<NvFile *> *files, string assemblyPath);


};

#endif