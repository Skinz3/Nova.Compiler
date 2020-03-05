#include "Exec.h"
#include "OpCodes.h"

void Exec::Execute(RuntimeContext* context, vector<RuntimeContext::RuntimeElement> locales, std::vector<int> ins)
{
	int ip = 0;
	int size = ins.size();

	while (ip < size)
	{
		switch (ins[ip])
		{
		case OpCodes::PushInt:
			context->PushStack(ins[++ip]);
			ip++;
			break;
		case OpCodes::Store:
			locales[ins[++ip]] = context->PopStack();
			ip++;
			break;
		case OpCodes::Load:
			context->PushStack(locales[ins[++ip]]);
			ip++;
			break;
		case OpCodes::Printl: // how?
			cout << std::get<int>(context->PopStack()) << endl;
			ip++;
			break;
		}
	}
}
