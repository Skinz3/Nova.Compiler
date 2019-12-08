#include <string>
#include "novafile.h"
#include "regex_constants.cpp"
#include <regex>
#include <string>

using namespace std;

NovaFile::NovaFile(string fileName)
{
    this->fileName = fileName;
    this->Read();
}
void NovaFile::Read()
{
    ReadLines();
    this->definition.className = Search(CLASS_PATTERN,1);

    if (this->definition.className == string())
    {
        cout << "Invalid file no class name." << endl;
    }

    string line = this->lines->at(8);

    Print();

}
void NovaFile::Print()
{
     cout << "class name :" << this->definition.className << endl;
}
void NovaFile::ReadLines()
{
    ifstream fstream(fileName);

    fstream.seekg(0, ios::beg); // seek to begining

    this->lines = new vector<string>();

    string line;

    while (getline(fstream, line))
    {
        lines->push_back(line);
    }

    fstream.close();
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
