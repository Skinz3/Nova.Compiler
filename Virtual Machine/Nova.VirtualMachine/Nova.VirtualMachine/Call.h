#pragma once

#include "ByteMethod.h"
#include "RuntimeContext.h"
#include <vector>

class Call
{
public:
	int returnIp;
	ByteMethod* method;
	ByteMethod* previousMethod;

	Call(ByteMethod* method, ByteMethod* previousMethod, int returnIp);
};

