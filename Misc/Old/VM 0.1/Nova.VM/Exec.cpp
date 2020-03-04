#include "Exec.h"


void Exec::Execute(RuntimeContext& context, RuntimeContext::RuntimeElement* locales, std::vector<Code*> codes)
{
	int index = 0;
	size_t size = codes.size();

	while (index < size)
	{
		Code* element = codes[index];
		element->Compute(context, locales, index);
	}
}

void Exec::Run(NovFile& file)
{
	RuntimeContext context(file);
	context.Initialize();
	context.Call("ExampleClass", "Main", 0);
}
