#pragma once
#include "Code.h"
class MethodCallStaticCode :
	public Code
{
private:
	std::string className;
	std::string methodName;
	int parametersCount;
public:
	void Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

