
#include "ComparaisonCode.h"
#include <iostream>

ComparaisonCode::ComparaisonCode(ComparaisonEnum type, int skip)
{
	this->type = type;
	this->skip = skip;
}

void ComparaisonCode::Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index)
{
	RuntimeContext::StackElement val1 = context.PopStack();
	RuntimeContext::StackElement val2 = context.PopStack();

	bool result = false;

	switch (type)
	{
		case ComparaisonEnum::Inferior:
			result = std::get<int>(val1) < std::get<int>(val2);
			break;
		default:
			std::cout << "NOT HANDLED OPERATOR" << std::endl;
	}

	if (result == true)
	{
		index += (1 + skip);
	}
	else
	{
		index++;
	}
}
