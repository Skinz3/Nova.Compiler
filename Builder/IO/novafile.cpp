
#include "novafile.h"
#include "parsing_helper.h"
#include "../Core/logger.h"

const string USING_PATTERN = "using (\\w.+)";
const string NAMESPACE_PATTERN = "namespace (\\w+)";
const string CLASS_PATTERN = "class (\\w+)";

NovaFile::NovaFile(string fileName)
{
    this->fileName = fileName;
}
bool NovaFile::Read()
{
    if (!ReadLines())
    {
        Logger::OnError(ErrorType::IO, this->fileName, "Cannot open file.");
        return false;
    }

    this->definition._namespace = ParsingHelper::SearchFirst(lines, NAMESPACE_PATTERN, 1).value;

    if (this->definition._namespace == string())
    {
        Logger::OnError(ErrorType::Syntaxic, this->fileName, "Invalid file, no namespace.");
        return false;
    }

    for (SearchResult result : ParsingHelper::Search(lines, USING_PATTERN, 1))
    {
        this->definition.usings.push_back(result.value);
    }

    if (!ReadBrackets())
    {
        return false;
    }

    return true;
}

bool NovaFile::ReadLines()
{
    std::ifstream fstream(fileName);

    if (!fstream.good())
    {
        return false;
    }

    this->lines = new vector<string>();

    string line;

    while (getline(fstream, line))
    {
        lines->push_back(line);
    }

    fstream.close();
    return true;
}
bool NovaFile::ReadBrackets()
{
    this->brackets = new map<int, int>();

    int currentIndent = 0;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        int count = std::count(line.begin(), line.end(), BRACKET_START_DELIMITER);

        if (count > 0)
        {
            currentIndent += count;
            brackets->insert(make_pair(i, currentIndent));
        }

        count = std::count(line.begin(), line.end(), BRACKET_END_DELIMITER);

        if (count > 0)
        {
            currentIndent -= count;
            brackets->insert(make_pair(i, currentIndent));
        }
    }

    if (!brackets->empty())
    {
        int lastIndentLevel = (--brackets->end())->second;
        if (lastIndentLevel != 0)
        {
            Logger::OnError(ErrorType::Syntaxic, this->fileName, "Invalid file brackets. (Last bracket indent level: " + std::to_string(lastIndentLevel) + ")");
            return false;
        }
    }
    return true;
}
bool NovaFile::ReadClasses()
{
    this->classes = new vector<Class *>();

    vector<SearchResult> results = ParsingHelper::Search(lines, CLASS_PATTERN, 1);

    if (results.size() == 0)
    {
        Logger::OnError(ErrorType::Syntaxic, this->fileName, "Invalid file. No classe(s) found.");
        return false;
    }

    for (SearchResult result : results)
    {
        string className = result.value;

        int classStartLine = ParsingHelper::FindNextOpenBracket(lines, result.index);

        int classEndLine = ParsingHelper::GetBracketCloseIndex(brackets, classStartLine);

        Class *novaClass = new Class(className, this->lines, this->brackets, classStartLine + 1, classEndLine);

        if (!novaClass->BuildMembers())
        {
            return false;
        }
        this->classes->push_back(novaClass);
    }

    return true;
}
vector<Class *> *NovaFile::GetClasses()
{
    return this->classes;
}
void NovaFile::Dispose()
{
    delete brackets;
    delete lines;
}
