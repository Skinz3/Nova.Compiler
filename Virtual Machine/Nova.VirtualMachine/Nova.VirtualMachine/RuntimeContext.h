#pragma once
#include <variant>
#include <string>
#include <vector>
#include "ByteMethod.h"
#include "Null.h"

class RuntimeStruct;

class NovFile;

class RuntimeContext
{
public:

	using RuntimeElement = std::variant<int, RuntimeStruct*, std::string*, bool, Null>;

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
	RuntimeContext::RuntimeElement StackMinus(int minus);

	/* Runtime Structs */
	RuntimeStruct* CreateStruct(int classId);
	RuntimeStruct* GetCurrentStruct();

	/* Static Fields */
	RuntimeContext::RuntimeElement Get(int classId, int fieldId);
	void Set(int classId, int fieldId, RuntimeContext::RuntimeElement value);

private:
	/* Stacks */
	std::vector<RuntimeElement> stack;
	std::vector<ByteMethod*> callStack;
	std::vector<RuntimeStruct*> structsStack;

	ByteClass* GetExecutingClass();

	void Call(ByteMethod* method);
};

