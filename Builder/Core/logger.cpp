#include "logger.h"
#include <string>
#include <iostream>

using namespace std;

const bool DEBUG = true;

void Logger::OnError(ErrorType type, string message)
{
    cout << GetErrorTypeStr(type) << " error : " << message << endl;
}
void Logger::OnError(ErrorType type, string filename, string message)
{
    cout << GetErrorTypeStr(type) << " error (" << filename << ") : " << message << endl;
}
void Logger::OnError(ErrorType type, string className, string line, int index, string message)
{
    cout << GetErrorTypeStr(type) << " error in class " << className << " (" << line << "," << index << ")" << message << endl;
}
void Logger::Log(string message)
{
    cout << message << endl;
}
void Logger::Debug(string message)
{
    if (DEBUG)
        cout << message << endl;
}
string Logger::GetErrorTypeStr(ErrorType type)
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