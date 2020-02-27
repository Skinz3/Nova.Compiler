#include "ByteMethod.h"
#include "ByteClass.h"



void ByteMethod::Deserialize(BinaryReader& reader)
{
	this->Meta.Deserialize(reader);
}

ByteMethod::ByteMethod(std::string name, ByteClass* parent)
{
	this->Name = name;
	this->Parent = parent;
}
