#include "NovFile.h"
#include "Logger.h"


NovFile::NovFile()
{

}

bool NovFile::Deserialize(BinaryReader& reader)
{
	std::string header = reader.ReadString();

	if (header != NovFile::HEADER)
	{
		Logger::Error("Invalid file. Header corrupted.");
		return false;
	}

	int classesCount = reader.Read<int>();

	for (int i = 0; i < classesCount; i++)
	{
		std::string key = reader.ReadString();
		ByteClass* byteClass = new ByteClass(key);
		byteClass->Deserialize(reader);
		ByteClasses.insert(std::make_pair(key, byteClass));

	}

	return true;
}
