#pragma once

#include "ByteMethod.h"
#include "RuntimeContext.h"
#include <vector>

class Call
{
public:
	vector<RuntimeContext::RuntimeElement> previousLocales;
	int returnIp;
	ByteMethod* method;
	ByteMethod* previousMethod;

	Call(ByteMethod* method, ByteMethod* previousMethod, int returnIp, std::vector<RuntimeContext::RuntimeElement> previousLocales);
};

