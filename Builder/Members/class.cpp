 #include "class.h"
#include <string>
#include <regex>
#include "method.h"


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
            string modifier = match[1];
            string returnType = match[2];
            string methodName = match[3];
            string parameters = match[4];

            Method * method = new Method(methodName,modifier,returnType,parameters);
            std::cout << line << std::endl;
           
            
          
        }
    }
    return true;
}
bool Class::BuildFields()
{
    return true;
}
