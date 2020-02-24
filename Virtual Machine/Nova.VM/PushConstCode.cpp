#include "PushConstCode.h"

PushConstCode::PushConstCode(RuntimeContext::StackElement value)
{
	this->value = value;
}

void PushConstCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	context.PushStack(value);
	index++;
}
