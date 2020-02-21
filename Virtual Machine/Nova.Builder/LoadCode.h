#pragma once
#include "Code.h"
class LoadCode :
	public Code
{
private:
	int variableId;
public:
	LoadCode(int value);
	void Compute(RuntimeContext& context, int& index);
};

