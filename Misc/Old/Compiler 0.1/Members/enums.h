#ifndef ENUMS
#define ENUMS

#include <string>

enum class ModifierEnum
{
    Public,
    Private
};

class Enums
{
public:
    static ModifierEnum ParseModifier(std::string modifier);
};

#endif // ENUMS