#pragma once

#include "RuntimeContext.h"

class RuntimeVector
{
private:
	vector<RuntimeContext::RuntimeElement> elements;

public:
	void Add(RuntimeContext::RuntimeElement element);
	RuntimeContext::RuntimeElement At(int index);
	int Size();
};
