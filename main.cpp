#include <iostream>
#include <string>
#include <fstream>
#include <vector>

using namespace std;

int main(int argc, char *argv[])
{
    string fileName = argv[1];

    cout << "Compiling " << fileName << "..." << endl;


    ifstream inFile(fileName);

    

    int lineCount = count(std::istreambuf_iterator<char>(inFile), std::istreambuf_iterator<char>(), '\n');

    int* a = NULL;
    a = new int[lineCount];
    vector<string> vector;

    std::string line;
    while (getline(inFile, line))
    {
        cout << line << endl;
        vector
    }
    system("pause");

    return EXIT_SUCCESS;
}