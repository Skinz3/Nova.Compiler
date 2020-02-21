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

	template<typename T>
	T Read();

};

