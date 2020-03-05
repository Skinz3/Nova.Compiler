#pragma once
#include "BinaryReader.h"
#include <vector>
class ByteBlock
{
public:
	void Deserialize(BinaryReader& reader);
	int localesCount;
	std::vector<int> instructions;
};

