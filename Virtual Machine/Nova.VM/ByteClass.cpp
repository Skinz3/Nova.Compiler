#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	this->Name = reader.ReadString();

	int methodCount = reader.Read<int>();

	for (int i = 0; i < methodCount; i++)
	{
		std::string methodName = reader.ReadString();

		ByteMethod method(methodName);

		method.Deserialize(reader);
		this->Methods.insert(std::make_pair(methodName, method));
	}

	int fieldsCount = reader.Read<int>();
}

ByteClass::ByteClass(std::string name)
{
}
