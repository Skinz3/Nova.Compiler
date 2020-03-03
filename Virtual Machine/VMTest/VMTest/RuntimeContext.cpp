#include "RuntimeContext.h"


void RuntimeContext::PushInt(int value)
{
	stack.emplace_back(value);
}
void RuntimeContext::PushStack(RuntimeContext::RuntimeElement & value)
{
	stack.emplace_back(value);
}

int RuntimeContext::PopStackInt()
{
	return std::get<int>(this->PopStack());
}



RuntimeContext::RuntimeElement RuntimeContext::PopStack()
{
	size_t stackSize = stack.size();
	RuntimeElement value = stack.at(stackSize - 1);
	stack.erase(stack.begin() + stackSize - 1);
	return value;
}

 

 
