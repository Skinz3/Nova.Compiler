#include "BinaryWriter.h"


BinaryWriter::BinaryWriter(string filePath)
{
	stream.open(filePath.c_str(), ios::binary);
}
template <typename T>
void  BinaryWriter::Write(T& value)
{
	stream.write((const char*)& value, sizeof(value));
}

void BinaryWriter::WriteString(string str)
{
	str += '\0';
	char* text = (char*)(str.c_str());
	unsigned long size = str.size();
	stream.write((const char*)text, size);
}
void BinaryWriter::Close()
{
	stream.close();
}
