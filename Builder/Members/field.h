#include <string>
#include "enums.h"
#include "../Statements/statement.h"

using namespace std;

class Field
{
public:
    Field(ModifierEnum modifier, string name, string type, Statement *value);
    bool ValidateSemantics();
private:
    ModifierEnum modifier;
    string name;
    string type;
    Statement* value;

};