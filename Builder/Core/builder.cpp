#include <iostream>
#include <string>
#include <vector>
#include "builder.h"
#include "../IO/novafile.h"

using namespace std;

bool Builder::Build(vector<NovaFile*> * files,string assemblyName)
{
      cout << "Building " << assemblyName  << "..."<<  endl;

      for (int i = 0;i < files->size();i++)
      {
           if (!files->at(i)->ReadClasses())
           {
               return false;
           }
      }
       
      // time to build ?f
      // here foreach files, we build symbols.
      // here we create assembly with nova files.
      // here we are writting assembly to disk.
      // compilation is finished.
}

