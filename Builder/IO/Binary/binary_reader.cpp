#ifndef BINREADER
#define BINREADER

#include <string>
#include <iostream>
#include <fstream>
#include "../../Core/builder_errors.h"

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
            BuilderErrors::OnError(ErrorType::IO,"Cannot open file : " + filePath);
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