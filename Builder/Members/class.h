#ifndef CLASS
#define CLASS

#include <string>
#include <vector>
#include <iostream>
#include <map>
#include "method.h"
#include "field.h"
#include "../IO/Binary/binary_writer.cpp"
#include "../IO/Binary/binary_reader.cpp"

class Class
{

public:
    Class(string className,std::vector<std::string> *fileLines, std::map<int, int> *fileBrackets, int startIndex, int endIndex);
    bool BuildMembers();

    void Serialize(BinaryWriter* writer);

private:
    string className;
    int startIndex;
    int endIndex;
    std::vector<std::string> *fileLines;
    std::map<int, int> *fileBrackets;
    std::vector<Method *> *methods;
    std::vector<Field *> *fields;
};

#endif // CLASS
