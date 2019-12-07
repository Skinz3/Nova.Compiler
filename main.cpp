#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <algorithm>
#include "IO/novafile.cpp"

using namespace std;

int main(int argc, char *argv[])
{
    string fileName = argv[1];

    cout << fileName << endl;

    ifstream fstream(fileName);

    int lineCount = count(istreambuf_iterator<char>(fstream), istreambuf_iterator<char>(), '\n') + 1;

    fstream.seekg(0, ios::beg); // seek to begining

    string *lines = new string[lineCount];

    int i = 0;
    std::string line;

    while (getline(fstream, line))
    {

        lines[i] = line;
        i++;
    }

    NovaFile *file = new NovaFile("test");
    cout << file->fileName << endl;

    system("pause");
    return EXIT_SUCCESS;
}
