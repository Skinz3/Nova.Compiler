#include "Exec.h"
#include "OpCodes.h"
#include "Logger.h"
#include "OperatorsEnum.h"

void Exec::Execute(RuntimeContext* context, vector<RuntimeContext::RuntimeElement> locales, std::vector<int> ins)
{
	int ip = 0;
	int size = ins.size();

	while (ip < size)
	{
		switch (ins[ip])
		{
		case OpCodes::Add:
			context->PushStack(std::get<int>(context->PopStack()) + std::get<int>(context->PopStack()));
			ip++;
			break;
		case OpCodes::PushInt:
			context->PushStack(ins[++ip]);
			ip++;
			break;
		case OpCodes::Store:
			locales[ins[++ip]] = context->PopStack();
			ip++;
			break;
		case OpCodes::Load:
		{
			int id = ins[++ip];
			context->PushStack(locales[id]);
			ip++;
			break;
		}
		case OpCodes::Printl: // how?
			cout << *std::get<string*>(context->PopStack()) << endl;
			ip++;
			break;
		case OpCodes::MethodCallMember:
			context->Call(ins[++ip]);
			ip++;
			break;
		case OpCodes::PushConst:
			context->PushStack(context->GetConstant(ins[++ip]));
			ip++;
			break;
		case OpCodes::Jump:
			ip = ins[++ip];
			break;
		case OpCodes::JumpIfFalse:
			
			if (std::get<bool>(context->PopStack()) == false)
			{
				ip = ins[++ip];
			}
			else
			{
				ip++;
			}
			break;
		case OpCodes::Comparaison:
		{
			int cmpType = ins[++ip];

			bool result = false;

			int val2 = std::get<int>(context->PopStack());
			int val1 = std::get<int>(context->PopStack());

			switch (cmpType)
			{
			case OperatorsEnum::Inferior:
				result = val1 < val2;
				break;
			}

			context->PushStack(result);
			ip++;
			break;
		}
		default:
			Logger::Error("Unhandled op code: " + std::to_string(ins[ip]));
			return;
		}
	}
}
