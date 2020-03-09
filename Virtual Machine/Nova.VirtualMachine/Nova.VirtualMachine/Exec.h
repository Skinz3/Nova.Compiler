#pragma once
#include "NovFile.h"
class Exec
{
public:
	static void Execute(RuntimeContext* context, ByteMethod* mainMethod);
private:
	static void CallMethod(RuntimeContext* context, ByteMethod* byteMethod,
		int* ip, std::vector<int>* ins, std::vector<RuntimeContext::RuntimeElement>* locales);
};

