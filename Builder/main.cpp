#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <algorithm>
#include "IO/novafile.h"
#include "Core/builder.h"

/* 
    ./compiler myScript.nv ---> ouput myScript.nov
    ./compiler myScript.nv MyLibrary.nov ---> output myLibrary.nov
    ./compiler myScript.nv mySecondScript.nv MyLibrary.nov ---> ouput MyLibrary.nov
    
    Un .nov est une assembly nova. Elle contient plusieurs fichiers.
    On passe au compilateur au moins 1 fichier .nv, il produit un .nov
    On peut passer au compilateur plusieurs fichiers. Il detectera
    automatiquement la fonction main. (Elle sera serialisée plus tard
    dans le .nov pour optimiser les performances de l'interpreteur.

    .nov :
    -definitions de base de l'assembly.
    -classes et leur symboles.

*/

#define DEFAULT_OUPUT_FILE "novaAssembly.nov"

int main(int argc, char *argv[])
{
    if (argc < 2)
    {
        Logger::Log("You need to specify at least one nova file (.nv).");
        return EXIT_SUCCESS;
    }

    string lastArg = argv[argc - 1];

    bool assemblyNameSpecified = lastArg.substr(lastArg.find_last_of(".") + 1) == "nov";

    string assemblyName;

    if (assemblyNameSpecified)
    {
        assemblyName = lastArg;
        Logger::Debug("Assembly name specified: " + assemblyName);

        if (argc == 2)
        {
            Logger::Log("You must provide at least one nova file (.nv) to compile.");
            return EXIT_SUCCESS;
        }
    }
    else
    {
        Logger::Debug("No Assemby name specified. ");
        assemblyName = DEFAULT_OUPUT_FILE;
    }

    vector<NovaFile *> *files = new vector<NovaFile *>();

    for (int i = 1; i < argc - (assemblyNameSpecified ? 1 : 0); i++)
    {
        string arg = argv[i];

        NovaFile file(arg); // instead of new NovaFile() for automatic storage duration

        if (!file.Read())
        {
            return EXIT_FAILURE;
        }

        Logger::Debug("File : " + arg);

        files->push_back(&file);
    }
    Builder builder(files, assemblyName);

    if (builder.Build() && builder.ValidateSemantic())
    {
        system("pause"); // for tests only.
        return EXIT_SUCCESS;
    }
    else
    {
        system("pause"); // for tests only.
        return EXIT_FAILURE;
    }
}
