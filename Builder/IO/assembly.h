#include <string>
#include <vector>
#include "../Members/class.h"
#include <map>

using namespace std;

class Assembly
{
public:
    Assembly(string name,map<string, vector<Class>>  classes);
    map<string, vector<Class>> classes; // private
    string name;

    void Serialize();
    
private:
    
   
};