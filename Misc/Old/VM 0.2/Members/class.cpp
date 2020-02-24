#include "class.h"
#include <string>
#include <regex>
#include "method.h"
#include "enums.h"
#include <map>
#include "../IO/parsing_helper.h"
#include "../Statements/statement_parser.h"
#include "../Utils/string_utils.h"
#include "../Core/semantic_analyser.h"

const std::regex METHOD_PATTERN{"^(public|private)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\((.*?)\\)"};

const std::regex FIELD_PATTERN{"^(public|private)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\\s*(=\\s*(.*))?$"};

Class::Class(ClassDefinitions *classDefinitions, string className, std::vector<std::string> *fileLines, std::map<int, int> *fileBrackets, int startIndex, int endIndex)
{
    this->classDefinitions = classDefinitions;
    this->className = className;
    this->fileLines = fileLines;
    this->fileBrackets = fileBrackets;
    this->startIndex = startIndex;
    this->endIndex = endIndex;
}
bool Class::BuildMembers()
{
    this->methods = new vector<Method *>();
    this->fields = new vector<Field *>();

    for (int i = this->startIndex; i < this->endIndex; i++)
    {
        std::string line = StringUtils::Trim(fileLines->at(i));

        std::smatch methodMatch;

        regex_search(line, methodMatch, METHOD_PATTERN);

        if (methodMatch.size() > 0)
        {
            ModifierEnum modifier = Enums::ParseModifier(methodMatch[1]);
            std::string returnType = methodMatch[2];
            std::string methodName = methodMatch[3];
            std::string parameters = methodMatch[4];

            int startIndex = ParsingHelper::FindNextOpenBracket(this->fileLines, i);
            int endIndex = ParsingHelper::GetBracketCloseIndex(this->fileBrackets, startIndex);

            Method *method = new Method(fileLines, fileBrackets, startIndex + 1, endIndex, methodName, modifier, returnType, parameters);

            if (!method->Build())
            {
                return false;
            }
            this->methods->push_back(method);
        }
        else
        {
            std::smatch fieldMatch;
            regex_search(line, fieldMatch, FIELD_PATTERN);

            if (fieldMatch.size() > 0)
            {

                ModifierEnum modifier = Enums::ParseModifier(fieldMatch[1]);
                std::string fieldType = fieldMatch[2];
                std::string fieldName = fieldMatch[3];
                std::string value = fieldMatch[5];

                Statement *st = StatementParser::ParseStatement(value);
                Field *field = new Field(modifier, fieldName, fieldType, st);
                this->fields->push_back(field);
            }
        }
    }

    return true;
}
void Class::Serialize(BinaryWriter *writer)
{
    writer->WriteString(this->className);

    int methodSize = this->methods->size();

    writer->Write<int>(methodSize);

    for (Method *method : *this->methods)
    {
        method->Serialize(writer);
    }
}
bool Class::ValidateSemantics()
{
    for (Field *field : *this->fields)
    {
        if (!field->ValidateSemantics())
        {
            return false;
        }
    }
    for (Method *method : *this->methods)
    {
        if (!method->ValidateSemantics())
        {
            return false;
        }
    }
    return true;
}