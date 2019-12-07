#include <string>
#include "novafile.h"

using namespace std;

NovaFile::NovaFile(string fileName)
{
    this->fileName = fileName;

    ifstream fstream(fileName);

    int lineCount = count(istreambuf_iterator<char>(fstream), istreambuf_iterator<char>(), '\n') + 1;

    fstream.seekg(0, ios::beg); // seek to begining

    this->lines = new vector<string>();

    int i = 0;
    string line;

    while (getline(fstream, line))
    {
        lines->push_back(line);
        
    }

    for (int i = 0;i < lines->size();i++)
    {
       cout << lines->at(i) << endl;
    }


    fstream.close();
}
