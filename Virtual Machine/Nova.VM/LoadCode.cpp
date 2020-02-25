#include "LoadCode.h"



void LoadCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	context.PushStack(locals[variableId]);
	index++;
}
void LoadCode::Deserialize(BinaryReader& reader)
{
	this->variableId = reader.Read<int>();
}
