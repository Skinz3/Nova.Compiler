#pragma once
#include "Code.h"

class JumpCode :
	public Code
{
private:
	int value;
public:
	void Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index);
	void Deserialize(BinaryReader& reader);
};

