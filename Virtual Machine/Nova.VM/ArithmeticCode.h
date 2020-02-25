#pragma once
#include "Code.h"
#include "OperatorEnum.h"

class ArithmeticCode : public  Code
{
public:
	OperatorEnum Op;
	void Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

