#include "PrintlCode.h"

void PrintlCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	RuntimeContext::StackElement element = context.PopStack();

	std::cout << std::get<char*>(element) << std::endl;
	index++;
}

void PrintlCode::Deserialize(BinaryReader& reader)
{

}
