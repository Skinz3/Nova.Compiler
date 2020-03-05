#include "ByteClass.h"
#include "ByteMethod.h"


void ByteMethod::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();
	this->modifiers = (Modifiers)reader.Read<char>();
	this->parametersCount = reader.Read<int>();

	this->block = new ByteBlock();
	this->block->Deserialize(reader);
}

ByteMethod::ByteMethod(ByteClass* parent)
{
	this->parent = parent;
}

void ByteMethod::Dispose()
{
	delete block;
}
