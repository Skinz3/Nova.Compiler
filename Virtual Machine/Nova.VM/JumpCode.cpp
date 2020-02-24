#include "JumpCode.h"

JumpCode::JumpCode(int value)
{
	this->value = value;
}

void JumpCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	index = this->value;
}
