#include "Exec.h"
#include "OpCodes.h"
#include "Logger.h"
#include "OperatorsEnum.h"
#include "RuntimeStruct.h"

template<class... Ts> struct overloaded : Ts... { using Ts::operator()...; };
template<class... Ts> overloaded(Ts...)->overloaded<Ts...>;

void Exec::Execute(RuntimeContext* context, vector<RuntimeContext::RuntimeElement> locales, std::vector<int> ins)
{
	int ip = 0;

	while (ip < ins.size())
	{
		switch (ins[ip])
		{
		case OpCodes::Add:
			context->PushStack(std::get<int>(context->PopStack()) + std::get<int>(context->PopStack()));
			ip++;
			break;
		case OpCodes::Sub:
		{
			int val1 = std::get<int>(context->PopStack());
			int val2 = std::get<int>(context->PopStack());
			context->PushStack(val2 - val1);
			ip++;
			break;
		}
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

			std::visit(overloaded
			{
					[](Null* arg) { std::cout << "null" << std::endl; },
					[](bool arg) { std::cout << (arg ? "true" : "false") << std::endl; },
					[](RuntimeStruct* arg) { std::cout << "{" << arg->typeClass->name << "}" << std::endl; },
					[](int arg) { std::cout << arg << std::endl; },
					[](std::string* arg) { std::cout << *arg << std::endl; },

			}, ele);

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

			if (std::get<bool>(context->PopStack()) == false)
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
			case OperatorsEnum::Equals:
				result = val1 == val2;
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
			RuntimeContext::RuntimeElement stElement = context->PopStack();

			if (std::holds_alternative<Null*>(stElement))
			{
				ip = ins.size();
				Logger::Error("Null reference exception."); /* Todo : handle this kind of errors. */
				return;
			}
			RuntimeStruct* st = std::get<RuntimeStruct*>(stElement);
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
		case OpCodes::PushNull:
		{
			context->PushStack(RuntimeContext::NULL_VALUE);
			ip++;
			break;
		}
		case OpCodes::Return:
		{
			ip = ins.size();
			break;
		}
		default:
			Logger::Error("Unhandled op code: " + std::to_string(ins[ip]));
			return;
		}
	}
}


