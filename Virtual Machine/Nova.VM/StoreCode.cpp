#include "StoreCode.h"

StoreCode::StoreCode(int variableId)
{
	this->variableId = variableId;
}

void StoreCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	RuntimeContext::StackElement value = context.PopStack();
	locals[variableId] = value;
	index++;
}
