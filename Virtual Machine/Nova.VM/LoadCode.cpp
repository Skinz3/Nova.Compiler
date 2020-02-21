#include "LoadCode.h"

LoadCode::LoadCode(int value)
{
	this->variableId = value;
}

void LoadCode::Compute(RuntimeContext& context, int& index)
{
	context.PushStack(context.locales[variableId]);
	index++;
}
