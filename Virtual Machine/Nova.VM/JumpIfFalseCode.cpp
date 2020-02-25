#include "JumpIfFalseCode.h"

void JumpIfFalseCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	if (std::get<int>(context.PopStack()) == 0)
	{
		index = this->TargetIndex;
	}
	else
	{
		index++;
	}
}

void JumpIfFalseCode::Deserialize(BinaryReader& reader)
{
	this->TargetIndex = reader.Read<int>();
}
