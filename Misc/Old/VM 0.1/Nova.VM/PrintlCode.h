#pragma once
#include "Code.h"
#include <string>

class PrintlCode :
	public Code
{
private:
public:
	void Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index);
	void Deserialize(BinaryReader& reader);
};

