#pragma once
#include "Code.h"
class JumpIfFalseCode :
	public Code
{
private:
	int TargetIndex;
public:
	void Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

