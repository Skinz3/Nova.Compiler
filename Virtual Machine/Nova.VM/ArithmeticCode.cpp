#include "ArithmeticCode.h"

#include <iostream>

void ArithmeticCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	int val2 = std::get<int>(context.PopStack());
	int val1 = std::get<int>(context.PopStack());

	int result = 0;

	switch (Op)
	{
	case OperatorEnum::Plus:
		result = val1 + val2;
		break;
	case OperatorEnum::Multiply:
		result = val1 * val2;
		break;
	case OperatorEnum::Minus:
		result = val1 - val2;
		break;
	default:
		throw "Unhandled Operator for Arithmetic Operation.";
	}

	context.PushStack(result);
	index++;
}

void ArithmeticCode::Deserialize(BinaryReader& reader)
{
	this->Op = (OperatorEnum)reader.Read<byte>();
}
