#include <string>
#include "novafile.h"
#include <regex>
#include <string>

using namespace std;

const string USING_PATTERN = "using (\\w.+)";
const string NAMESPACE_PATTERN = "namespace (\\w+)";
const string CLASS_PATTERN = "class (\\w+)";

const string BRACKET_START_DELIMITER = "{";
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

    std::map<int, int>::iterator it = brackets.begin();

    while(it != brackets.end())
    {
        std::cout<<it->first<<" :: "<<it->second<<std::endl;
        it++;
    }

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
bool NovaFile::ReadBrackets()
{
    int currentIdent = 0;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        if (line.find(BRACKET_START_DELIMITER) != std::string::npos)
        {
            currentIdent++;
            this->brackets.insert(make_pair(i,currentIdent));
          
        }
        if (line.find(BRACKET_END_DELIMITER) != std::string::npos)
        {
            currentIdent--;
            this->brackets.insert(make_pair(i,currentIdent));
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
