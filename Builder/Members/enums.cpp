#include "enums.h"
#include <string>

using namespace std;

Modifier Enums::ParseModifier(string modifier)
{
    if (modifier == "public")
        return Modifier::Public;
    else if (modifier == "private")
        return Modifier::Private;
}