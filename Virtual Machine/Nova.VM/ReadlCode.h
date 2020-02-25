#pragma once
#include "Code.h"
class ReadlCode :
	public Code
{
public:
	void Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

