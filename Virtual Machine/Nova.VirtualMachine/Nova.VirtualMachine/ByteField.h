#pragma once
#include "BinaryReader.h"
#include "Modifiers.h"
#include "ByteBlock.h"

class ByteField
{
public:
	string name;
	ByteBlock valueBlock;
	Modifiers modifiers;

	void Deserialize(BinaryReader& reader);
};

