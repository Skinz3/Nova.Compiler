#include "ByteField.h"

void ByteField::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();
	this->modifiers = (Modifiers)reader.Read<char>();
	this->valueBlock.Deserialize(reader);
}
