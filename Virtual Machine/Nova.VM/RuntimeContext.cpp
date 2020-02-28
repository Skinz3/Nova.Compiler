
#include "RuntimeContext.h"

#include "Exec.h"

RuntimeContext::RuntimeContext(NovFile& file)
{
	this->file = &file;
}

void RuntimeContext::PushStack(RuntimeContext::RuntimeElement& element)
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

void RuntimeContext::Call(std::string className, std::string methodName, int paramsCount)
{
	ByteMethod* method = this->file->ByteClasses[className]->Methods[methodName];
	this->Call(method, paramsCount);
}

void RuntimeContext::Call(std::string methodName, int paramsCount)
{
	ByteMethod* method = this->ExecutingClass->Methods[methodName];
	this->Call(method, paramsCount);
}

void RuntimeContext::Call(ByteMethod* method, int parametersCount)
{
	this->ExecutingClass = method->Parent;

	RuntimeContext::RuntimeElement* locales = new RuntimeContext::RuntimeElement[method->Meta.localsCount];

	for (int i = parametersCount - 1; i >= 0; i--)
	{
		locales[i] = PopStack();
	}

	Exec::Execute(*this, locales, method->Meta.Codes);
}


void RuntimeContext::Initialize()
{
}








