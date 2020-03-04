#pragma once
#include "BinaryReader.h"
#include "BinaryReader.h"
class MainPointEntry
{
public:
	int classIndex;
	int methodIndex;
	void Deserialize(BinaryReader& reader);
};

