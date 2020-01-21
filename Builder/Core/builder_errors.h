#ifndef BUILDER_ERRORS
#define BUILDER_ERRORS

#include <string>
#include "error_type.h"

using namespace std;

class BuilderErrors
{
public:
    static void OnError(ErrorType type, string message);

    static void OnError(ErrorType type, string fileName, string message);

    static void OnError(ErrorType type, string className, string line,int index, string message);

private:
    static string GetErrorTypeStr(ErrorType errorType);
};

#endif