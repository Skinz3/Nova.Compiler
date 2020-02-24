#include "semantic_analyser.h"

map<string, vector<Class>> SemanticAnalyser::classes;

void SemanticAnalyser::Initialize(vector<NvFile*>* files)
{
    map<string, vector<Class>> classes;

    for (NvFile *file : *files) // verify class name is distincted by namespace
    {
        vector<Class *> *fileClasses = file->GetClasses();

        for (Class *_class : *fileClasses)
        {
            if (classes.count(file->definitions._namespace))
            {
                classes[file->definitions._namespace].push_back(*_class);
            }
            else
            {
                vector<Class> newVect;
                newVect.push_back(*_class);
                classes.insert(pair<string, vector<Class>>(file->definitions._namespace, newVect));
            }
        }
    }

    SemanticAnalyser::classes = classes;
}
map<string, vector<Class>> SemanticAnalyser::GetClasses()
{
    return SemanticAnalyser::classes;
}
bool SemanticAnalyser::ValidateSemantics()
{
    for (auto pair : SemanticAnalyser::classes)
    {
        for (Class c : pair.second)
        {
            if (!c.ValidateSemantics())
            {
                return false;
            }
        }
    }
    return true;
}

bool SemanticAnalyser::IsVariableAccessible(Class currentClass,Method currentMethod,string variableName)
{
    
}