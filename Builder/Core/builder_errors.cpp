#include "builder_errors.h"
#include <string>
#include <iostream>

using namespace std;

void BuilderErrors::OnError(ErrorType type, string message)
{
    cout << GetErrorTypeStr(type) << " error : " << message << endl;
}
void BuilderErrors::OnError(ErrorType type, string filename, string message)
{
    cout << GetErrorTypeStr(type) << " error (" << filename << ") : " << message << endl;
}
void BuilderErrors::OnError(ErrorType type, string className, string line, int index, string message)
{
    cout << GetErrorTypeStr(type) << " error in class " << className << " (" << line << "," << index << ")" << message << endl;
}
string BuilderErrors::GetErrorTypeStr(ErrorType type)
{
    switch (type)
    {
    case ErrorType::IO:
        return "IO";
    case ErrorType::Semantic:
        return "Semantic";
    case ErrorType::Syntaxic:
        return "Syntaxic";
    }
}