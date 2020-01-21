#include <iostream>
#include <vector>
#include <string>
#include "../IO/nvfile.h"

using namespace std;

class Builder
{
public:
    Builder(vector<NvFile *> *files, string assemblyName);
    bool Build();
    bool ValidateSemantic();
private:
    string assemblyPath;
    vector<NvFile *> *files;
};