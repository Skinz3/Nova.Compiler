#pragma once

#include "BinaryReader.h"
#include "ByteBlock.h"
#include "Modifiers.h"

class ByteClass;

class ByteMethod
{
public:
	std::string name;
	Modifiers modifiers;
	int parametersCount;
	ByteBlock* block;
	ByteClass* parent;
	void Deserialize(BinaryReader& reader);

	ByteMethod(ByteClass* parent);

	void Dispose();
};

