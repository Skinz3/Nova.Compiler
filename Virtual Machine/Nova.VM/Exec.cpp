#include "Exec.h"

void Exec::Execute(RuntimeContext &context,RuntimeContext::StackElement locals[], std::vector<Code*> codes)
{
	int index = 0;
	int size = codes.size();

	while (index < size)
	{
		Code* element = codes[index];
		element->Compute(context,locals, index);
	}
}

void Exec::Run(NovFile& file)
{

}
