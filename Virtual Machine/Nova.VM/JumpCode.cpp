#include "JumpCode.h"

JumpCode::JumpCode(int value)
{
	this->value = value;
}

void JumpCode::Compute(RuntimeContext& context, int& index)
{
	index = this->value;
}
