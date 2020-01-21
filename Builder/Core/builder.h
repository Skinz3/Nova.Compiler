#include <iostream>
#include <vector>
#include <string>
#include "../IO/novafile.h"

using namespace std;

class Builder
{
public:
    Builder(vector<NovaFile *> *files, string assemblyName);
    bool Build();
    bool ValidateSemantic();
private:
    string assemblyPath;
    vector<NovaFile *> *files;
};