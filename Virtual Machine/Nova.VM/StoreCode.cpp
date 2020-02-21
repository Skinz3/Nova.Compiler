#include "StoreCode.h"

StoreCode::StoreCode(int variableId)
{
	this->variableId = variableId;
}

void StoreCode::Compute(RuntimeContext& context, int& index)
{
	RuntimeContext::StackElement value = context.PopStack();
	context.locales[variableId] = value;
	index++;
}
