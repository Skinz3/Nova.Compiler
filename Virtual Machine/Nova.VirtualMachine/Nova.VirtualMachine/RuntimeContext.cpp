#include "RuntimeContext.h"
#include "NovFile.h"
#include "Exec.h"
#include "RuntimeStruct.h"

Null* RuntimeContext::NULL_VALUE = new Null(); /* Should we create a class to store this kinda types? FALSE , TRUE ? */


RuntimeContext::RuntimeContext(NovFile* file)
{
	this->novFile = file;
}

void RuntimeContext::Initialize()
{
	for (ByteClass* byteClass : this->novFile->byteClasses)
	{
		for (ByteField* field : byteClass->fields)
		{
			field->Initializer(this);
		}
	}
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

	callStack.resize(callStack.size() - 1);// we pop call stack. 
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

RuntimeContext::RuntimeElement RuntimeContext::StackMinus(int minus)
{
	return this->stack[this->stack.size() - 1 - minus];
}

size_t RuntimeContext::GetStackSize()
{
	return this->stack.size();
}

RuntimeStruct* RuntimeContext::CreateStruct(int classId)
{
	return new RuntimeStruct(novFile->byteClasses[classId]);
}

RuntimeStruct* RuntimeContext::GetCurrentStruct()
{
	return structsStack.at(structsStack.size() - 1);
}

RuntimeContext::RuntimeElement RuntimeContext::Get(int classId, int fieldId)
{
	return this->novFile->byteClasses[classId]->fields[fieldId]->value;
}

void RuntimeContext::Set(int classId, int fieldId, RuntimeContext::RuntimeElement value)
{
	this->novFile->byteClasses[classId]->fields[fieldId]->value = value;
}
