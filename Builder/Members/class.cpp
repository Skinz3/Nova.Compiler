#include "class.h"
#include <string>
#include <regex>
#include "method.h"
#include "enums.h"
#include <map>
#include "../IO/parsing_helper.h"

const std::string METHOD_PATTERN = "(public|private) (\\w+) (\\w+)\\((.*?)\\)";

Class::Class(std::vector<std::string> *fileLines, std::map<int, int> *fileBrackets, int startIndex, int endIndex)
{
    this->fileLines = fileLines;
    this->fileBrackets = fileBrackets;
    this->startIndex = startIndex;
    this->endIndex = endIndex;
}
bool Class::Build()
{
    return BuildMethods() && BuildFields();
}
bool Class::BuildMethods()
{
    this->methods = new vector<Method *>();

    for (int i = this->startIndex; i < this->endIndex; i++)
    {
        std::string line = fileLines->at(i);

        std::regex r{METHOD_PATTERN, std::regex_constants::ECMAScript};

        std::smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            ModifierEnum modifier = Enums::ParseModifier(match[1]);
            std::string returnType = match[2];
            std::string methodName = match[3];
            std::string parameters = match[4];

            int startIndex = ParsingHelper::FindNextOpenBracket(this->fileLines, i);
            int endIndex = ParsingHelper::GetBracketCloseIndex(this->fileBrackets, startIndex);

            Method *method = new Method(fileLines, fileBrackets, startIndex + 1, endIndex, methodName, modifier, returnType, parameters);
            
            if (!method->Build())
            {
                return false;
            }
            this->methods->push_back(method);
        }
    }

    return true;
}
bool Class::BuildFields()
{
    return true;
}
