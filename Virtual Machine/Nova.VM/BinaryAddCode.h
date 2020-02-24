#pragma once
#include "Code.h"

class BinaryAddCode : public  Code
{
public:
	BinaryAddCode();
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
};

