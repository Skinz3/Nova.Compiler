#include "NovFile.h"
#include "BinaryReader.h"
#include "Logger.h"

const string HEADER = "NovaEX";

NovFile::NovFile(std::string& fileName)
{
	this->fileName = fileName;
}

bool NovFile::Deserialize()
{
	BinaryReader reader(this->fileName);

	if (reader.ReadString() != HEADER)
	{
		Logger::Error("Corrupted .nov file: Wrong header.");
		return false;
	}

	mainPointEntry.Deserialize(reader);

	int classesCount = reader.Read<int>();

	for (int i = 0; i < classesCount; i++)
	{
		ByteClass byteClass;
		byteClass.Deserialize(reader);
		this->byteClasses.push_back(byteClass);
	}

	return true;

}

void NovFile::Print()
{
	for (ByteClass byteClass : byteClasses)
	{
		Logger::Log("> " + byteClass.name + " (" + std::to_string(byteClass.fields.size()) + " fields)");

		for (ByteMethod method : byteClass.methods)
		{
			Logger::Debug("> " + method.name + "()");
		}

	}
}

void NovFile::Dispose()
{
	for (ByteClass byteClass : byteClasses)
	{
		byteClass.Dispose();
	}
}

ByteMethod NovFile::GetMainMethod()
{
	return this->byteClasses[mainPointEntry.classIndex].methods[mainPointEntry.methodIndex];
}
