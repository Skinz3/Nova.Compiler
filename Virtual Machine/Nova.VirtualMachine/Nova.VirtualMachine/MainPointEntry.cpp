#include "MainPointEntry.h"

void MainPointEntry::Deserialize(BinaryReader& reader)
{
	this->classIndex = reader.Read<int>();
	this->methodIndex = reader.Read<int>();
}
