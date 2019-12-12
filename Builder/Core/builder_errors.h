#include <string>

using namespace std;

class BuilderErrors
{
public:
    static void OnError(string message);

public:
    static void OnError(string fileName, string message);

public:
    static void OnError(string className, string line, string message);
};