#pragma once
#include "Code.h"
class ReturnCode :
	public Code
{
public:
	void Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

