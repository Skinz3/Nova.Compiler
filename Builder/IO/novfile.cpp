#include "novfile.h"
#include <fstream>


NovFile::NovFile(string name, map<string, vector<Class>> classes)
{
    this->name = name;
    this->classes = classes;
}

void NovFile::Serialize(BinaryWriter* writer)
{
    int classesSize = classes.size();
    writer->Write<int>(classesSize);

    for (auto pair : this->classes)
    {
        writer->WriteString(pair.first);
        
        int csize = pair.second.size();

        writer->Write<int>(csize);

        for (Class element : pair.second)
        {
            element.Serialize(writer);
        }
    }
}
 