#pragma once
#include "NovFile.h"
class Exec
{
public:
	static void Execute(RuntimeContext* context, vector<RuntimeContext::RuntimeElement> locales, std::vector<int> ins);
};

