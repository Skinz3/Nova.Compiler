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

    cout << "Compiling " << fileName << "..." <<  endl;

    NovaFile *file = new NovaFile(fileName);

    system("pause");
    return EXIT_SUCCESS;
}
