#pragma once
#include <variant>
#include <string>
#include <vector>
#include "ByteMethod.h"

class RuntimeStruct;

class NovFile;

class RuntimeContext
{
public:

	using RuntimeElement = std::variant<int, std::string*>;

	NovFile* novFile;

	RuntimeContext(NovFile* file);

	/* Executing program initialization */
	void Initialize();

	/* Method call */
	void CallMain();
	void Call(int classId, int methodId);
	void Call(int methodId);
	void Call(RuntimeStruct* st, int methodId);

	/* Block constants */
	RuntimeContext::RuntimeElement GetConstant(int id);

	/* Stack management */
	void PushStack(RuntimeContext::RuntimeElement element);
	RuntimeContext::RuntimeElement PopStack();


private:
	/* Stacks */
	std::vector<RuntimeElement> stack;
	std::vector<ByteMethod*> callStack; 
	std::vector<RuntimeStruct*> structsStack; 

	ByteClass* GetExecutingClass();

	void Call(ByteMethod& method);
};

