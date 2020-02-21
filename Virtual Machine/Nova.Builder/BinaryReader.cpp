#include "BinaryReader.h"


BinaryReader::BinaryReader(string filePath)
{
	stream.open(filePath.c_str(), ios::binary);
}

template <typename T>
T BinaryReader::Read()
{
	T value;
	stream.read((char*)& value, sizeof(value));
	return value;
}

string BinaryReader::ReadString()
{
	char c;
	string result = "";
	while (!stream.eof() && (c = Read<char>()) != '\0')
	{
		result += c;
	}

	return result;
}
void BinaryReader::Close()
{
	stream.close();
}
