#pragma once
#include "Code.h"
class StoreStaticCode :
	public Code
{
private:
	std::string className;
	std::string fieldName;
public:
	void Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

