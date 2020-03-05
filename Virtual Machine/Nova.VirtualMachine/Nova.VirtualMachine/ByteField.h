#pragma once
#include "BinaryReader.h"
#include "Modifiers.h"
#include "ByteBlock.h"
#include "RuntimeContext.h"

class ByteField
{
public:
	string name;
	ByteBlock* valueBlock;
	Modifiers modifiers;

	RuntimeContext::RuntimeElement value;

	void Deserialize(BinaryReader& reader);

	void Dispose();
};

