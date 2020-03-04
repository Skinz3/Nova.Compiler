#include "BinaryReader.h"


BinaryReader::BinaryReader(string filePath)
{
	stream.open(filePath.c_str(), ios::binary);
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
