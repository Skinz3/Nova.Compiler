#pragma once
#include <string>
#include <map>
#include "ByteClass.h"
#include "BinaryReader.h"


class NovFile
{
private:
	const std::string HEADER = "NovaEX";

public:
	std::map <std::string, ByteClass> ByteClasses;
	NovFile();

	bool Deserialize(BinaryReader &reader);

};

