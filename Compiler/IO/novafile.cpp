#include <string>
#include "novafile.h"
#include <regex>
#include <string>

using namespace std;

#define CLASS_PATTERN "class (\\w+)"
#define NAMESPACE_PATTERN "namespace (\\w+)"

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

    this->definition.className = Search(CLASS_PATTERN,1);
    this->definition.classNamespace = Search(NAMESPACE_PATTERN,1);

    if (this->definition.className == string())
    {
        cout << "Invalid file no class name." << endl;
        return false;
    }

    if (this->definition.classNamespace == string())
    {
        cout << "Invalid file no namespace." << endl;
        return false;

    }

   // Print();

    return true;

}
void NovaFile::Print()
{
     cout << "Class name: " << this->definition.className << endl;
     cout << "Namespace: " << this->definition.classNamespace << endl;
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
string NovaFile::Search(string pattern, int index)
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
