#include "StoreCode.h"


void StoreCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	RuntimeContext::StackElement value = context.PopStack();
	locals[variableId] = value;
	index++;
}
void StoreCode::Deserialize(BinaryReader& reader)
{
	this->variableId = reader.Read<int>();
}