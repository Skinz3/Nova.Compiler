#include <iostream>
#include <vector>
#include <string>
#include <map>
#include "../Members/class.h"
#include "../IO/nvfile.h"

using namespace std;

class SemanticAnalyser
{
public:
   static void Initialize(vector<NvFile *> *files);
   static bool ValidateSemantics();
   static map<string, vector<Class>> GetClasses();
   static bool IsVariableAccessible(Class currentClass,Method currentMethod,string variableName);
private:
    static map<string, vector<Class>> classes;
    
};