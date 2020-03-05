#include "ByteBlock.h"

void ByteBlock::Deserialize(BinaryReader& reader)
{
	int size = reader.Read<int>();

	for (int i = 0; i < size; i++)
	{
		instructions.push_back(reader.Read<int>());
	}

	this->localesCount = reader.Read<int>();
}
