#pragma once
#include "RuntimeContext.h"
#include "Code.h"
#include "NovFile.h"
class Exec
{
public :
	static void Execute(RuntimeContext &context,RuntimeContext::StackElement locals[], std::vector<Code*> codes);
	static void Run(NovFile &file);
};

