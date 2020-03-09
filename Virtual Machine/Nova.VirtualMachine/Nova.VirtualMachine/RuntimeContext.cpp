#include "RuntimeContext.h"
#include "NovFile.h"
#include "Exec.h"
#include "RuntimeStruct.h"
#include "Call.h"

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



RuntimeContext::RuntimeElement RuntimeContext::GetConstant(int id)
{
	return this->novFile->constants[id];
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

