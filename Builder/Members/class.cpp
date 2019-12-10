#include "class.h"
#include <string>

Class::Class(vector<string> lines)
{
    this->lines = lines;
}
bool Class::Build()
{
    return BuildMethods() && BuildFields();
}
bool Class::BuildMethods()
{

}
bool Class::BuildFields()
{
    
}