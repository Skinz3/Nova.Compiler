#include "LoadCode.h"

LoadCode::LoadCode(int value)
{
	this->variableId = value;
}

void LoadCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	context.PushStack(locals[variableId]);
	index++;
}
