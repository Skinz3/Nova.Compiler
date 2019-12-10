#include <string>

 enum class Modifier
 {
      Public,
      Private
};

class Enums
{
public:
    static Modifier ParseModifier(string modifier);
}; 