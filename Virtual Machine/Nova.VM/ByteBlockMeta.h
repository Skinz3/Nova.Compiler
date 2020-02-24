#pragma once
#include "BinaryReader.h"
#include <map>
#include <vector>
#include "Code.h"

class ByteBlockMeta
{
private:
	std::map<string, int> LocalsRelator;

public:
	void Deserialize(BinaryReader& reader);
	std::vector<Code*> Codes;

};

