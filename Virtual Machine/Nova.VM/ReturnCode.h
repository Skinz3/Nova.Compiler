#pragma once
#include "Code.h"
class ReturnCode :
	public Code
{
public:
	void Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

