#include <iostream>
#include <string>
#include <vector>
#include <map>
#include "builder.h"
#include "../IO/nvfile.h"
#include "../IO/novfile.h"
#include "../IO/Binary/binary_writer.cpp"
#include <stdio.h>
#include "semantic_analyser.h"

using namespace std;


bool Builder::Build(vector<NvFile *> *files, string assemblyPath)
{
    Logger::Debug("Building " + assemblyPath + " ...");

    for (int i = 0; i < files->size(); i++)
    {
        if (!files->at(i)->ReadClasses())
        {
            return false;
        }
    }

    SemanticAnalyser::Initialize(files);

    if (!SemanticAnalyser::ValidateSemantics())
    {
        return false;
    }

    NovFile *result = new NovFile(assemblyPath);

    remove(assemblyPath.c_str());

    BinaryWriter *writer = new BinaryWriter(assemblyPath);

    result->Serialize(writer);

    writer->Close();

    Logger::Log(assemblyPath + " generated.");

    delete writer;

    return true;

    // time to build ?f
    // here foreach files, we build symbols.
    // here we create assembly with nova files.
    // here we are writting assembly to disk.
    // compilation is finished.
}
