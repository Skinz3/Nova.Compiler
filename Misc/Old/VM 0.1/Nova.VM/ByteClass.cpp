#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	int methodCount = reader.Read<int>();

	for (int i = 0; i < methodCount; i++)
	{
		std::string methodName = reader.ReadString();
		ByteMethod* method = new ByteMethod(methodName, this);
		method->Deserialize(reader);
		this->Methods.insert(std::make_pair(methodName, method));
	}

	int fieldsCount = reader.Read<int>();

	for (int i = 0; i < fieldsCount; i++)
	{
		std::string fieldName = reader.ReadString();
		ByteField* field = new ByteField(fieldName);
		field->Deserialize(reader);
		this->Fields.insert(std::make_pair(fieldName, field));
	}
}

ByteClass::ByteClass(std::string name)
{
	this->Name = name;
}
