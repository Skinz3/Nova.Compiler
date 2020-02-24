#include "ByteMethod.h"


ByteMethod::ByteMethod(std::string name)
{
	this->Name = name;
}

void ByteMethod::Deserialize(BinaryReader& reader)
{
	this->Meta.Deserialize(reader);
}
