#pragma once
#include "Code.h"
class MethodCallCode :
	public Code
{
private:
	std::string methodName;
	int parametersCount;
public:
	void Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

