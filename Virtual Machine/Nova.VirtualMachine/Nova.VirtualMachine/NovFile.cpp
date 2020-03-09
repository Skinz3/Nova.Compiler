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

	for (int i = 0; i < reader.Read<int>(); i++)
	{
		ByteClass* byteClass = new ByteClass();
		byteClass->Deserialize(reader);
		this->byteClasses.push_back(byteClass);
	}

	reader.Close();

	return true;

}

void NovFile::Print()
{
	for (ByteClass* byteClass : byteClasses)
	{
		Logger::Log("> " + byteClass->name + " (" + std::to_string(byteClass->fields.size()) + " fields)");

		for (ByteMethod* method : byteClass->methods)
		{
			Logger::Debug("> " + method->name + "()");
		}

	}
}

void NovFile::Dispose()
{
	for (ByteClass* byteClass : byteClasses)
	{
		byteClass->Dispose();
		delete byteClass;
	}
}

ByteMethod* NovFile::GetMainMethod()
{
	return this->byteClasses[mainPointEntry.classIndex]->methods[mainPointEntry.methodIndex];
}