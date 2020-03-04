#pragma once
#include "BinaryReader.h"
#include "ByteMethod.h"
#include "ByteField.h"
#include <vector>

class ByteClass
{
public:
	std::string Name;
	std::vector<ByteMethod> Methods;
	std::vector<ByteField> Fields;
	void Deserialize(BinaryReader& reader);
	ByteClass(std::string name);
};
