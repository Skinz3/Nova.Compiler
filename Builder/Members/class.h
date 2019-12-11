#ifndef CLASS
#define CLASS

#include <string>
#include <vector>
#include <iostream>



class Class
{

public:
    Class(std::vector<std::string> lines);
    bool Build();

private:
    std::vector<std::string> lines;
    bool BuildMethods();
    bool BuildFields();
};

#endif // CLASS


