#pragma once
#include "BinaryReader.h"
#include "ByteBlockMeta.h"

class ByteMethod
{
public:
	std::string Name;
	ByteBlockMeta Meta;
	void Deserialize(BinaryReader& reader);

	ByteMethod(std::string name);
};

