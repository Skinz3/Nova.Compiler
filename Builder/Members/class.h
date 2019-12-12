#ifndef CLASS
#define CLASS

#include <string>
#include <vector>
#include <iostream>
#include <map>
#include "method.h"

class Class
{

public:
    Class(std::vector<std::string> *fileLines, std::map<int, int> *fileBrackets, int startIndex, int endIndex);
    bool Build();

private:
    int startIndex;
    int endIndex;
    std::vector<std::string> *fileLines;
    std::map<int, int> *fileBrackets;
    std::vector<Method*> *methods;

    bool BuildMethods();
    bool BuildFields();
};

#endif // CLASS
