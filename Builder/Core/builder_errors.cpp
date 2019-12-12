#include "builder_errors.h"
#include <string>
#include <iostream>

using namespace std;

void BuilderErrors::OnError(string message)
{
    cout << "Error: " << message << endl;
}