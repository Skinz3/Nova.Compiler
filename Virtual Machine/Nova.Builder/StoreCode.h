#pragma once
#include "Code.h"
class StoreCode :
	public Code
{
private:
	int variableId;
public:
	StoreCode(int variableId);
	void Compute(RuntimeContext& context, int& index);
};

