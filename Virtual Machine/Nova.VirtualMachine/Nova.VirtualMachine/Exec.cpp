#include "Exec.h"
#include "OpCodes.h"
#include "Logger.h"
#include "OperatorsEnum.h"
#include "RuntimeStruct.h"

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
		case OpCodes::Mul:
			context->PushStack(std::get<int>(context->PopStack()) * std::get<int>(context->PopStack()));
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
		case OpCodes::Printl:
		{
			RuntimeContext::RuntimeElement ele = context->PopStack();

			if (std::holds_alternative<std::string*>(ele)) /* Optimized in release x86. 0ms for 1 million iterations. */
			{
				cout << *std::get<string*>(ele) << endl;
			}
			else if (std::holds_alternative<int>(ele))
			{
				cout << std::get<int>(ele) << endl;
			}
			else if (std::holds_alternative<bool>(ele))
			{
				bool value = std::get<bool>(ele);
				cout << (value ? "true" : "false") << endl;
			}
			else if (std::holds_alternative<RuntimeStruct*>(ele))
			{
				cout << "{" << std::get<RuntimeStruct*>(ele)->typeClass->name << "}" << endl;
			}
			else
			{
				Logger::Error("Unable to print element.");
			}
			ip++;
			break;
		}
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

			if (std::get<bool>(context->PopStack()) == 0)
			{
				ip = ins[++ip];
			}
			else
			{
				ip += 2;
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
		case OpCodes::StructCallMethod:
		{
			RuntimeStruct* st = std::get<RuntimeStruct*>(context->PopStack());
			context->Call(st, ins[++ip]);
			ip++;
			break;
		}
		case OpCodes::StructCreate:
		{
			context->PushStack(context->CreateStruct(ins[++ip]));
			ip++;
			break;
		}
		case OpCodes::CtorCall:
		{
			int parametersCount = ins[++ip];
			int methodId = ins[++ip];

			RuntimeStruct* obj = std::get<RuntimeStruct*>(context->StackMinus(parametersCount));
			context->Call(obj, methodId);
			ip++;
			break;
		}
		case OpCodes::StructPushCurrent:
		{
			context->PushStack(context->GetCurrentStruct());
			ip++;
			break;
		}
		case OpCodes::StructLoadMember:
		{
			RuntimeStruct* st = std::get<RuntimeStruct*>(context->PopStack());
			int fieldId = ins[++ip];
			RuntimeContext::RuntimeElement member = st->Get(fieldId);
			context->PushStack(member);
			ip++;
			break;
		}
		case OpCodes::StructStoreMember:
		{
			RuntimeStruct* st = std::get<RuntimeStruct*>(context->PopStack());
			RuntimeContext::RuntimeElement ele = context->PopStack();
			st->Set(ins[++ip], ele);
			ip++;
			break;
		}
		case OpCodes::MethodCallStatic:
		{
			int classId = ins[++ip];
			int methodId = ins[++ip];
			context->Call(classId, methodId);
			ip++;
			break;
		}
		case OpCodes::StoreStatic:
		{
			int classId = ins[++ip];
			int fieldId = ins[++ip];
			context->Set(classId, fieldId, context->PopStack());
			ip++;
			break;
		}
		case OpCodes::LoadStatic:
		{
			int classId = ins[++ip];
			int fieldId = ins[++ip];
			context->PushStack(context->Get(classId, fieldId));
			ip++;
			break;
		}
		default:
			Logger::Error("Unhandled op code: " + std::to_string(ins[ip]));
			return;
		}
	}
}


