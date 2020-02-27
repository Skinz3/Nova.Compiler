#include "JumpIfFalseCode.h"

void JumpIfFalseCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	int value = std::get<int>(context.PopStack()); // value must be char.

	if (value == 0)
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
