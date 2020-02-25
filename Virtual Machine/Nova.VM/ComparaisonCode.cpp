
#include "ComparaisonCode.h"
#include <iostream>


void ComparaisonCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	int val1 = std::get<int>(context.PopStack());
	int val2 = std::get<int>(context.PopStack());


	bool result = false;

	switch (Op)
	{
		case OperatorEnum::Inferior:
			result = val1 < val2;
			break;
		case OperatorEnum::Different:
			result = val1 != val2;
			break;
		case OperatorEnum::Superior:
			result = val1 > val2;
			break;
		case OperatorEnum::Equals:
			result = val1 == val2;
			break;
		default:
			throw "Unhandled operator for ComparaisonCode.";
	}

	context.PushStack(result == true ? 1 : 0);
	index++;
}

void ComparaisonCode::Deserialize(BinaryReader& reader)
{
	this->Op = (OperatorEnum)reader.Read<byte>();
}
