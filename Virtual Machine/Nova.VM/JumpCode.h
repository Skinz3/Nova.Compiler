#pragma once
#include "Code.h"

class JumpCode :
	public Code
{
private:
	int value;
public:
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

