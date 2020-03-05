#pragma once
#include "ByteClass.h"

class RuntimeStruct
{
private:
	vector<RuntimeContext::RuntimeElement> properties;

public:
	RuntimeStruct(ByteClass* typeClass);
	ByteClass* typeClass;
	void Set(int fieldId, RuntimeContext::RuntimeElement value);
	RuntimeContext::RuntimeElement Get(int fieldId);
};

