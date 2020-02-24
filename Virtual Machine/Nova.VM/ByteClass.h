#pragma once
#include "BinaryReader.h"
#include "ByteMethod.h"
#include <map>



class ByteClass
{
public:
	std::string Name;
	std::map<std::string, ByteMethod> Methods;
	void Deserialize(BinaryReader& reader);

	ByteClass(std::string name);
};

