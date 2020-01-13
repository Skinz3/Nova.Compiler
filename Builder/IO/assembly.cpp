#include "assembly.h"
#include <fstream>

Assembly::Assembly(string name,map<string, vector<Class>>  classes)
{
    this->name = name;
    this->classes = classes;
}

void Assembly::Serialize()
{
     std::fstream fs;
     fs.open ("build.nov",  std::fstream::out | std::fstream::app);
     fs << (int)2;
     fs.close();
}