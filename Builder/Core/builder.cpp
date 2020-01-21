#include <iostream>
#include <string>
#include <vector>
#include <map>
#include "builder.h"
#include "../IO/novafile.h"
#include "../IO/assembly.h"
#include "../IO/Binary/binary_writer.cpp"
#include <stdio.h>

using namespace std;

Builder::Builder(vector<NovaFile *> *files, string assemblyPath)
{
    this->files = files;
    this->assemblyPath = assemblyPath;
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
    
    for (NovaFile* file : *files) 
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

    Assembly* result = new Assembly(assemblyPath,classes);
    
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
