#include "ReturnCode.h"

void ReturnCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	index++;
	std::cout << "Return code reached... something todo?" << std::endl;
}

void ReturnCode::Deserialize(BinaryReader& reader)
{
}
