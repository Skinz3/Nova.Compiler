

#ifndef NVFILE
#define NVFILE


#include <string>
#include <vector>
#include <map>
#include "../Members/class.h"
#include "../Members/class_definitions.h"
#include <regex>
#include <algorithm>
#include <iostream>
#include <fstream>


using namespace std;



class NvFile
{

public:
    NvFile(string fileName);
    string fileName;
    void Print();
    bool Read();
    bool ReadClasses();
    void Dispose();
    vector<Class*>* GetClasses();
    ClassDefinitions definitions;
    
private:
    vector<string> *lines;
    bool ReadLines();
    bool ReadBrackets();

    map<int,int>* brackets;

    vector<Class*>* classes;
};

#endif // NVFILE

