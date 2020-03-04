#include "ByteClass.h"
#include "ByteMethod.h"


void ByteMethod::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();
	this->modifiers = (Modifiers)reader.Read<char>();
	this->parametersCount = reader.Read<int>();
	this->block.Deserialize(reader);
}

ByteMethod::ByteMethod(ByteClass * parent)
{
	this->parent = parent;
}
