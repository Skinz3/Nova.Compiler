#include "class.h"
#include <string>
#include <regex>
#include "method.h"

const std::string METHOD_PATTERN = "(public|private) (\\w+) (\\w+)\\((.*?)\\)";

Class::Class(std::vector<std::string> lines)
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
        std::string line  = lines[i];

        std::regex r{METHOD_PATTERN, std::regex_constants::ECMAScript};

        std::smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            std::string modifier = match[1];
            std::string returnType = match[2];
            std::string methodName = match[3];
            std::string parameters = match[4];

            Method * method = new Method(methodName,modifier,returnType,parameters);
            std::cout <<"Method:"<< line << std::endl;
        }
    }
    return true;
}
bool Class::BuildFields()
{
    return true;
}
