#pragma once

#include "ByteMethod.h"
#include "RuntimeContext.h"
#include <vector>

class MethodCall
{
public:
	vector<RuntimeContext::RuntimeElement> locales;
	int returnIp;
	std::vector<int> previousInstructions;

	MethodCall(std::vector<int> previousInstructions, int returnIp,vector<RuntimeContext::RuntimeElement> locales);
};

