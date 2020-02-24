
#include <string>
#include <vector>
#include "../Members/class.h"
#include <map>
#include "./Binary/binary_writer.cpp"
#include "./Binary/binary_reader.cpp"

using namespace std;

class NovFile
{
public:
    NovFile(string name);
    string name;

    void Serialize(BinaryWriter* writer);
    
private:
    
   
};
