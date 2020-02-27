#pragma once
#include "Code.h"
class ReadlCode :
	public Code
{
public:
	void Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

