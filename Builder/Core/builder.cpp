#include <iostream>
#include <string>
#include <vector>
#include <map>
#include "builder.h"
#include "../IO/novafile.h"
#include "../IO/assembly.h"


using namespace std;

bool Builder::Build(vector<NovaFile *> *files, string assemblyName)
{
    cout << "Building " << assemblyName << "..." << endl;

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

    Assembly* result = new Assembly(assemblyName,classes);
    
    
    result->Serialize();
    
    return true;

    // time to build ?f
    // here foreach files, we build symbols.
    // here we create assembly with nova files.
    // here we are writting assembly to disk.
    // compilation is finished.
}
