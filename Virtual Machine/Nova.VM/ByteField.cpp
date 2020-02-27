#include "ByteField.h"

ByteField::ByteField(std::string name)
{
	this->name = name;
}

void ByteField::Deserialize(BinaryReader& reader)
{
	this->meta.Deserialize(reader);
}
