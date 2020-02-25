#include "RuntimeContext.h"
#include <iostream>

void RuntimeContext::PushStack(RuntimeContext::StackElement element)
{
	stack.push_back(element);
}

RuntimeContext::StackElement RuntimeContext::PopStack()
{
	RuntimeContext::StackElement value = stack.at(stack.size() - 1);
	stack.erase(stack.begin() + stack.size() - 1);
	return value;
}





