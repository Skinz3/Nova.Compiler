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

	if (!reader.IsValid())
	{
		Logger::Error(this->fileName+" do not exists.");
		return false;
	}
	if (reader.ReadString() != HEADER)
	{
		Logger::Error("Corrupted .nov file: Wrong header.");
		return false;
	}

	mainPointEntry.Deserialize(reader);

	int classesCount = reader.Read<int>();

	for (int i = 0; i < classesCount; i++)
	{
		ByteClass* byteClass = new ByteClass();
		byteClass->Deserialize(reader);
		this->byteClasses.push_back(byteClass);
		
	}

	int constantsCount = reader.Read<int>();

	for (int i = 0; i < constantsCount; i++)
	{
		int type = reader.Read<int>();

		switch (type)
		{
		case 1:
		{
			string* value = new string(); // allocates a string.
			*value = reader.ReadString();
			constants.push_back(value);
			break;
		}
		case 2:
		{
			constants.push_back((bool)reader.Read<bool>());
			break;
		}
		default:
		{
			throw std::invalid_argument("Unknown constant type.");
		}
		}
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
	for (RuntimeContext::RuntimeElement element : this->constants)
	{
		delete& element;
	}
}

ByteMethod* NovFile::GetMainMethod()
{
	return this->byteClasses[mainPointEntry.classIndex]->methods[mainPointEntry.methodIndex];
}
