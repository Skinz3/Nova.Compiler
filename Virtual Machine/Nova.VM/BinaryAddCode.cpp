#include "BinaryAddCode.h"

#include <iostream>

void BinaryAddCode::Compute(RuntimeContext& context, int& index)
{
	RuntimeContext::StackElement val1 = context.PopStack();
	RuntimeContext::StackElement val2 = context.PopStack();

	context.PushStack(std::get<int>(val1) + std::get<int>(val2));
	index++;
}

BinaryAddCode::BinaryAddCode()
{

}
