#include "assembly.h"

Assembly::Assembly(string name,vector<Class*>* classes)
{
    this->name = name;
    this->classes = classes;
}