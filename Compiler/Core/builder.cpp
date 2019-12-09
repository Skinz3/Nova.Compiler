#include <iostream>
#include <string>
#include <vector>
#include "builder.h"

using namespace std;

bool Builder::Build(vector<NovaFile*> files,string assemblyName)
{
      cout << "Building " << assemblyName  << "..."<<  endl;
      // time to build ?
      // here foreach files, we build symbols.
      // here we create assembly with nova files.
      // here we are writting assembly to disk.
      // compilation is finished.
}