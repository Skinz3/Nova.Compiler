#include "PrintlCode.h"

void PrintlCode::Compute(RuntimeContext& context, RuntimeContext::RuntimeElement* locales, int& index)
{
	RuntimeContext::RuntimeElement element = context.PopStack();

	if (std::holds_alternative<int>(element))
	{
		std::cout << std::get<int>(element) << std::endl;
	}
	else if (std::holds_alternative<string>(element))
	{
		std::cout << std::get<string>(element) << std::endl;
	}

	index++;
}

void PrintlCode::Deserialize(BinaryReader& reader)
{

}
