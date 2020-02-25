#pragma once
#include "Code.h"
#include "OperatorEnum.h"

class ComparaisonCode :
	public Code
{
	private:
		OperatorEnum Op;
public:
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

