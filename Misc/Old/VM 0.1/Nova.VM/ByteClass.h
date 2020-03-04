#pragma once
#include "BinaryReader.h"
#include "ByteMethod.h"
#include "ByteField.h"
#include <map>



class ByteClass
{
public:
	std::string Name;
	std::map<std::string, ByteMethod*> Methods;
	std::map<std::string, ByteField*> Fields;
	void Deserialize(BinaryReader& reader);
	ByteClass(std::string name);
};

