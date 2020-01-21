#include <iostream>
#include <string>
#include <vector>
#include <map>
#include "builder.h"
#include "../IO/nvfile.h"
#include "../IO/novfile.h"
#include "../IO/Binary/binary_writer.cpp"
#include <stdio.h>

using namespace std;

Builder::Builder(vector<NvFile *> *files, string assemblyPath)
{
    this->files = files;
    this->assemblyPath = assemblyPath;
}
bool Builder::ValidateSemantic()
{
    return true;
}
bool Builder::Build()
{
    cout << "Building " << assemblyPath << "..." << endl;

    for (int i = 0; i < files->size(); i++)
    {
        if (!files->at(i)->ReadClasses())
        {
            return false;
        }
    }

    map<string, vector<Class>> classes;
    
    for (NvFile* file : *files) 
    {
         vector<Class*>* fileClasses = file->GetClasses();

         for (Class* _class : *fileClasses)
         {
             if (classes.count(file->definition._namespace))
             {
                classes[file->definition._namespace].push_back(*_class);
             }
             else
             {
                 vector<Class> newVect;
                 newVect.push_back(*_class);
                 classes.insert(pair<string,vector<Class>>(file->definition._namespace,newVect));
             }
         }

    }

    NovFile* result = new NovFile(assemblyPath,classes);
    
    remove(assemblyPath.c_str());

    BinaryWriter* writer = new BinaryWriter(assemblyPath);

    result->Serialize(writer);
    
    writer->Close();

    cout << assemblyPath << " generated." << endl;

    delete writer;
    
    return true;

    // time to build ?f
    // here foreach files, we build symbols.
    // here we create assembly with nova files.
    // here we are writting assembly to disk.
    // compilation is finished.
}
