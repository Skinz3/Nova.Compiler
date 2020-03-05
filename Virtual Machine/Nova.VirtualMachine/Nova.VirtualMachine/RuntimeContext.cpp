#include "RuntimeContext.h"
#include "NovFile.h"
#include "Exec.h"
#include "RuntimeStruct.h"

RuntimeContext::RuntimeContext(NovFile* file)
{
	this->novFile = file;
}

void RuntimeContext::Initialize()
{
	/* Todo */
}

void RuntimeContext::CallMain()
{
	ByteMethod* method = this->novFile->GetMainMethod();
	Call(method);
}

void RuntimeContext::Call(int classId, int methodId)
{
	ByteMethod* method = this->novFile->byteClasses[classId]->methods[methodId];
	Call(method);
}

void RuntimeContext::Call(int methodId)
{
	ByteClass* executingClass = this->GetExecutingClass();
	ByteMethod* method = executingClass->methods[methodId];
	Call(method);
}

void RuntimeContext::Call(RuntimeStruct* st, int methodId)
{
	structsStack.push_back(st);

	ByteMethod* method = st->typeClass->methods[methodId];
	Call(method);

	structsStack.erase(structsStack.begin() + structsStack.size() - 1);
}

void RuntimeContext::Call(ByteMethod* method)
{
	callStack.push_back(method); // we push call stack

	vector<RuntimeContext::RuntimeElement> locales(method->block->localesCount);

	for (int i = method->parametersCount - 1; i >= 0; i--)
	{
		locales[i] = PopStack();
	}

	Exec::Execute(this, locales, method->block->instructions);

	callStack.erase(callStack.begin() + callStack.size() - 1); // we pop call stack. 
}

ByteClass* RuntimeContext::GetExecutingClass()
{
	return callStack.at(callStack.size() - 1)->parent;
}
RuntimeContext::RuntimeElement RuntimeContext::GetConstant(int id)
{
	return this->GetExecutingClass()->constants[id];
}

void RuntimeContext::PushStack(RuntimeContext::RuntimeElement  element)
{
	stack.emplace_back(element);
}

RuntimeContext::RuntimeElement RuntimeContext::PopStack()
{
	size_t stackSize = stack.size();
	RuntimeContext::RuntimeElement value = stack.at(stackSize - 1);
	stack.erase(stack.begin() + stackSize - 1);
	return value;
}
