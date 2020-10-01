#pragma once

#include <string>
#include <iostream>
#include <fstream>

using namespace std;

class BinaryReader
{
private:
	ifstream stream;
public:
	BinaryReader(string filePath);

	string ReadString();

	void Close();

	bool IsValid();

	template<typename T>
	T Read()
	{
		T value;
		stream.read((char*)& value, sizeof(value));
		return value;
	}

};

