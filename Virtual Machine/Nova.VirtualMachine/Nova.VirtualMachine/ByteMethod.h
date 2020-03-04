#pragma once

#include "BinaryReader.h"
#include "ByteBlock.h"

class ByteClass;

class ByteMethod
{
public:
	std::string Name;
	ByteBlock* Meta;
	ByteClass* Parent;
	void Deserialize(BinaryReader& reader);

	ByteMethod(ByteClass * parent);
};

