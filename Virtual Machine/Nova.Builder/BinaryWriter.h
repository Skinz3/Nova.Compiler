#pragma once

#include <string>
#include <iostream>
#include <fstream>

using namespace std;

class BinaryWriter
{
public:
	BinaryWriter(string filePath);
	void WriteString(string str);
	void Close();
	template<typename T>
	void Write(T& value);

private:
	ofstream stream;
	
};

