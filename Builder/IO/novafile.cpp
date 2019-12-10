#include <string>
#include "novafile.h"
#include <regex>
#include <string>

using namespace std;

const string USING_PATTERN = "using (\\w.+)";
const string NAMESPACE_PATTERN = "namespace (\\w+)";
const string CLASS_PATTERN = "class (\\w+)";

const string BRACKET_START_DELIMITER = "{"; // multiple brackets on line is yet not supported.
const string BRACKET_END_DELIMITER = "}";

NovaFile::NovaFile(string fileName)
{
    this->fileName = fileName;
}
bool NovaFile::Read()
{
    if (!ReadLines())
    {
        cout << "Unable to read file (IO error): " << this->fileName << endl;
        return false;
    }

    this->definition._namespace = SearchFirst(NAMESPACE_PATTERN, 1);

    if (this->definition._namespace == string())
    {
        cout << "Invalid file no namespace." << endl;
        return false;
    }

    this->definition.usings = Search(USING_PATTERN, 1);

    if (!ReadBrackets())
    {
        return false;
    }

    return true;
}

bool NovaFile::ReadBrackets()
{
    this->brackets;

    int currentIndent = 0;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        if (line.find(BRACKET_START_DELIMITER) != std::string::npos)
        {
            currentIndent++;

            brackets.insert(make_pair(i, currentIndent));
        }
        if (line.find(BRACKET_END_DELIMITER) != std::string::npos)
        {
            currentIndent--;
            brackets.insert(make_pair(i, currentIndent));
        }
    }

    if (!brackets.empty())
    {
        int lastIndentLevel= (--brackets.end())->second;
        if (lastIndentLevel != 0)
        {

            cout << "Invalid file brackets. (Last bracket indent level: " << lastIndentLevel << ")" << endl;
            return false;
        }
    }
    return true;
}
bool NovaFile::ReadLines()
{
    ifstream fstream(fileName);

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
bool NovaFile::ReadClasses() // Reflechir a un algorithme optimisé de parsing de classes.
{
    this->classes = new vector<Class>();

    vector<string> matches = Search(CLASS_PATTERN, 1);

    if (matches.size() == 0)
    {
        cout << "Invalid file, no classes." << endl;
        return false;
    }

    // ici on créer les classes. On parse tout les statements

    return true;
}
vector<string> NovaFile::FindLinesUnderIndent(int startLineIndex, int minIndent)
{
    for (int i = startLineIndex; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);
    }
}
int NovaFile::GetIndentLevel(int lineIndex)
{
    if (lineIndex > (lines->size() - 1))
    {
        return -1;
    }

    map<int, int>::iterator current = brackets.begin();

    while (current != brackets.end())
    {
        int index1 = current->first;

        current++;

        int index2 = current->first;

        if (lineIndex >= index1 && lineIndex < index2)
        {
            current--;
            return current->second;
        }
    }
    return 0;
}
string NovaFile::SearchFirst(string pattern, int index)
{
    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        regex r{pattern, regex_constants::ECMAScript};
        smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            return match[index];
        }
    }
    return string();
}
vector<string> NovaFile::Search(string pattern, int index)
{
    vector<string> matches;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        regex r{pattern, regex_constants::ECMAScript};
        smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            matches.push_back(match[index]);
        }
    }
    return matches;
}
