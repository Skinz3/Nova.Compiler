#ifndef BINREADER
#define BINREADER

#include <string>
#include <iostream>
#include <fstream>

using namespace std;

class BinaryReader
{
private:
    ifstream stream;

public:
    BinaryReader(string filePath)
    {
        stream.open(filePath.c_str(), ios::binary);
        if (!stream.is_open())
        {
            cout << "Error, cannot open file " << endl;
        }
    }

    template <typename T>
    T Read()
    {
        T value;
        stream.read((char *)&value, sizeof(value));
        return value;
    }
    string ReadString()
    {
        char c;
        string result = "";
        while (!stream.eof() && (c = Read<char>()) != '\0')
        {
            result += c;
        }

        return result;
    }
    void Close()
    {
        stream.close();
    }
};


#endif // BINREADER