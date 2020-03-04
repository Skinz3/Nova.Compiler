#include <string>
#include "BinaryReader.h"
#include "ByteBlockMeta.h"
#include "RuntimeContext.h"

#pragma once
class ByteField
{
public:
	RuntimeContext::RuntimeElement value;
	ByteField(std::string name);
	void Deserialize(BinaryReader& reader);

private:
	std::string name;
	ByteBlockMeta meta;
};

