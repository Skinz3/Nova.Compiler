#include "assembly.h"
#include <fstream>
#include "./Binary/binary_writer.cpp"
#include "./Binary/binary_reader.cpp"

Assembly::Assembly(string name, map<string, vector<Class>> classes)
{
    this->name = name;
    this->classes = classes;
}

void Assembly::Serialize()
{
    BinaryReader reader("build.nov");
    

    cout << reader.ReadString() << endl;
    cout << reader.Read<float>() << endl;


    reader.Close();

    
}