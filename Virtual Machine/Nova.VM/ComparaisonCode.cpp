
#include "ComparaisonCode.h"
#include <iostream>

static RuntimeContext::RuntimeElement TRUE = 1;
static RuntimeContext::RuntimeElement FALSE = 0;

void ComparaisonCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	int val2 = std::get<int>(context.PopStack());
	int val1 = std::get<int>(context.PopStack());

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

	if (result)
	{
		context.PushStack(TRUE);
	}
	else
	{
		context.PushStack(FALSE);
	}

	index++;
}

void ComparaisonCode::Deserialize(BinaryReader& reader)
{
	this->Op = (OperatorEnum)reader.Read<byte>();
}
