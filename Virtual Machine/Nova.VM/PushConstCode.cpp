#include "PushConstCode.h"

void PushConstCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	context.PushStack(value);
	index++;
}
void PushConstCode::Deserialize(BinaryReader& reader)
{
	this->value = reader.Read<int>(); // todo 
}