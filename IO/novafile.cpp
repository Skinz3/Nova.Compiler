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
    ReadDefinition();
}
void NovaFile::ReadLines()
{
    ifstream fstream(fileName);

    int lineCount = count(istreambuf_iterator<char>(fstream), istreambuf_iterator<char>(), '\n') + 1;

    fstream.seekg(0, ios::beg); // seek to begining

    this->lines = new vector<string>();

    string line;

    while (getline(fstream, line))
    {
        lines->push_back(line);
    }

    fstream.close();
}
void NovaFile::ReadDefinition()
{
     for (int i = 0;i < this->lines->size();i++)
    {
        string line = this->lines->at(i);

       // regex pattern = regex("(?<=\\bclass\\s)(\\w+)");
       // smatch match; 
        //regex_search(line,match,pattern);

        
    } 
  

   
}
