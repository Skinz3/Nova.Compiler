#ifndef CLASS
#define CLASS

#include <string>
#include <vector>
#include <iostream>

using namespace std;

class Class
{

public:
    Class(vector<string> lines);
    bool Build();

private:
    vector<string> lines;
    bool BuildMethods();
    bool BuildFields();
};

#endif // CLASS


