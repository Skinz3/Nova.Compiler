#include "PrintlCode.h"

void PrintlCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	RuntimeContext::RuntimeElement element = context.PopStack();
	std::cout << std::get<std::string>(element) << std::endl;
	index++;
}

void PrintlCode::Deserialize(BinaryReader& reader)
{

}
