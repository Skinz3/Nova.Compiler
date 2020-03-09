#pragma once
#include "NovFile.h"
class Exec
{
public:
	static void Run(NovFile & file);
	
private:
	static void Execute(RuntimeContext* context, ByteBlock* block);
	static void CallMethod(RuntimeContext* context, ByteMethod* byteMethod,
		int* ip, std::vector<int>* ins, std::vector<RuntimeContext::RuntimeElement>* locales);
};

