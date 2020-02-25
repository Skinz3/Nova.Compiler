#pragma once
#include "Code.h"
#include <string>

class PrintlCode :
	public Code
{
private:
public:
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

