#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();

	this->type = (ContainerType)reader.Read<char>();

	int methodsCount = reader.Read<int>();

	for (int i = 0; i < methodsCount; i++)
	{
		ByteMethod* method = new ByteMethod(this);
		method->Deserialize(reader);
		this->methods.push_back(method);
	}

	int fieldsCount = reader.Read<int>();

	for (int i = 0; i < fieldsCount; i++)
	{
		ByteField* field = new ByteField();
		field->Deserialize(reader);
		this->fields.push_back(field);
	}

	
}

void ByteClass::Dispose()
{
	for (ByteMethod* method : this->methods)
	{
		method->Dispose();
		delete method;
	}
	for (ByteField* field : this->fields)
	{
		field->Dispose();
		delete field;
	}
	
}
