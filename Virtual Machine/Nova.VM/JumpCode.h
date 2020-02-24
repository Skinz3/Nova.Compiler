#pragma once
#include "Code.h"

class JumpCode :
	public Code
{
private:
	int value;
public:
	JumpCode(int value);
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
};

