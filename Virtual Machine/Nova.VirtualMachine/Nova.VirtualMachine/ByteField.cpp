#include "ByteField.h"

void ByteField::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();
	this->modifiers = (Modifiers)reader.Read<char>();
	this->valueBlock = new ByteBlock();
	this->valueBlock->Deserialize(reader);
}

void ByteField::Dispose()
{
	delete valueBlock;
}
