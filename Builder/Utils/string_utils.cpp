#include "string_utils.h"

using namespace std;

const string CHARS =  "\t\n\v\f\r ";

string StringUtils::LeftTrim(string str)
{
    str.erase(0, str.find_first_not_of(CHARS));
    return str;
}
 
string StringUtils::RightTrim(string str)
{
    str.erase(str.find_last_not_of(CHARS) + 1);
    return str;
}
 
string StringUtils::Trim(string str)
{
    return StringUtils::LeftTrim(StringUtils::RightTrim(str));
}

bool StringUtils::IsEmptyOrWhiteSpace(string str)
{
    return str == "" || str.find_first_not_of(' ') == std::string::npos;
}