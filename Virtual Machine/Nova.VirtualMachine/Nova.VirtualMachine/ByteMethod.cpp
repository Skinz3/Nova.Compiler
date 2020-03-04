#include "ByteMethod.h"
#include "ByteClass.h"



void ByteMethod::Deserialize(BinaryReader& reader)
{
	//this->Meta.Deserialize(reader);
}

ByteMethod::ByteMethod(ByteClass * parent)
{
	this->Parent = parent;
}
