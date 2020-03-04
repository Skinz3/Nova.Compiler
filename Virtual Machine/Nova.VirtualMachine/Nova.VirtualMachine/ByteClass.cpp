#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	int methodCount = reader.Read<int>();

	for (int i = 0; i < methodCount; i++)
	{
		ByteMethod method(this);
		method.Deserialize(reader);
		this->Methods.push_back(method);
	}

	int fieldsCount = reader.Read<int>();

	for (int i = 0; i < fieldsCount; i++)
	{
		ByteField field;
	//	field.Deserialize(reader);
		this->Fields.push_back(field);
	}
}

ByteClass::ByteClass(std::string name)
{
	this->Name = name;
}
