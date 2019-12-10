#include "class.h"
#include <string>
#include <regex>

const string METHOD_PATTERN = "(public|private) (\\w+) (\\w+)\\((.*?)\\)";

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
    for (int i = 0;i < lines.size();i++)
    {
        string line  = lines[i];

        regex r{METHOD_PATTERN, regex_constants::ECMAScript};

       
        smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
             cout << line << endl;
        }
    }
    return true;
}
bool Class::BuildFields()
{
    
}