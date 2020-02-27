#pragma once
#include "BinaryReader.h"
#include "ByteBlockMeta.h"

class ByteClass;

class ByteMethod
{
public:
	std::string Name;
	ByteBlockMeta Meta;
	ByteClass* Parent;
	void Deserialize(BinaryReader& reader);

	ByteMethod(std::string name,ByteClass* parent);
};

