#include <string>
#include "ByteClass.h"
#include "MainPointEntry.h"

#pragma once
class NovFile
{
public:
	NovFile(std::string& fileName);
	bool Deserialize();
	std::vector<ByteClass> byteClasses;
	MainPointEntry mainPointEntry;
	void Print();
	void Dispose();
private:
	std::string fileName;
};

