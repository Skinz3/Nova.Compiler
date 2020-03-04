#pragma once
#include "Code.h"
class StoreCode :
	public Code
{
private:
	int variableId;
public:
	void Compute(RuntimeContext& context, RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

