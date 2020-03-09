#pragma once
#include "NovFile.h"
class Exec
{
public:
	static void Run(NovFile& file);
	static void Execute(RuntimeContext* context, ByteBlock* block);
private:
	
	static void CallMethod(RuntimeContext* context, ByteMethod* byteMethod,
		int & ip, int & lOffset, std::vector<int> & ins, std::vector<RuntimeContext::RuntimeElement>* locales);
};

