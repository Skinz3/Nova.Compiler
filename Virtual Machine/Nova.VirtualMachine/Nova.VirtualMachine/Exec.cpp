#include "Exec.h"
#include "OpCodes.h"
#include "Logger.h"
#include "OperatorsEnum.h"
#include "RuntimeStruct.h"
#include "Natives.h"


template<class... Ts> struct overloaded : Ts... { using Ts::operator()...; };
template<class... Ts> overloaded(Ts...)->overloaded<Ts...>;


void Exec::Run(NovFile& file)
{
	RuntimeContext context(&file);

	context.Initialize();

	ByteMethod* mainMethod = file.GetMainMethod();

	Call call(mainMethod, nullptr, -1);

	context.callStack.push_back(call);

	Execute(&context, mainMethod->block);
}

void Exec::Execute(RuntimeContext* context, ByteBlock* block)
{
	int ip = 0;

	int lOffset = 0;

	std::vector<RuntimeContext::RuntimeElement> locales(block->localesCount);

	std::vector<int> ins = block->instructions;

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
			locales[lOffset + ins[++ip]] = context->PopStack(); // see warning.
			ip++;
			break;
		case OpCodes::Load:
		{
			int id = ins[++ip];
			context->PushStack(locales[lOffset + id]);
			ip++;
			break;
		}
		case OpCodes::NativeCall:
		{
			DispatchNative(context, ins[++ip]);
			ip++;
			break;
		}
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
			case OperatorsEnum::Superior:
				result = val1 > val2;
				break;
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
			context->structsStack.push_back(obj);
			ByteMethod* method = obj->typeClass->methods[methodId];
			CallMethod(context, method, ip, lOffset, ins, &locales);
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
		case OpCodes::StructCallMethod:
		{
			RuntimeStruct* st = std::get<RuntimeStruct*>(context->PopStack());
			context->structsStack.push_back(st);
			ByteMethod* method = st->typeClass->methods[ins[++ip]];
			CallMethod(context, method, ip, lOffset, ins, &locales); // <-------------------------------------
			break;
		}

		case OpCodes::MethodCall:
		{
			int classId = ins[++ip];
			int methodId = ins[++ip];
			ByteMethod* method = context->novFile->byteClasses[classId]->methods[methodId];
			CallMethod(context, method, ip, lOffset, ins, &locales); // <-------------------------------------
			break;
		}
		case OpCodes::StoreGlobal:
		{
			int classId = ins[++ip];
			int fieldId = ins[++ip];
			context->Set(classId, fieldId, context->PopStack());
			ip++;
			break;
		}
		case OpCodes::LoadGlobal:
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
			if (context->callStack.size() == 1) // main method call is on heap.
			{
				return;
			}

			Call lastCall = context->callStack[context->callStack.size() - 1];

			if (lastCall.method->parent->type == ContainerType::Struct) // <--- far from memory
			{
				context->structsStack.resize(context->structsStack.size() - 1);
			}

			ip = lastCall.returnIp;
			ins = lastCall.previousMethod->block->instructions;

			//	FreeHeap(&locales, lOffset); <-------------------------- todo

			lOffset -= lastCall.previousMethod->block->localesCount;

			locales.resize(locales.size() - lastCall.method->block->localesCount);


			context->callStack.resize(context->callStack.size() - 1);
			break;
		}
		default:
			Logger::Error("Unhandled op code: " + std::to_string(ins[ip]));
			return;
		}
	}
}

void Exec::CallMethod(RuntimeContext* context, ByteMethod* targetMethod, int& ip, int& lOffset, std::vector<int>& ins, std::vector<RuntimeContext::RuntimeElement>* locales)
{
	ByteMethod* executingMethod = context->callStack[context->callStack.size() - 1].method;

	Call methodCall(targetMethod, executingMethod, ip + 1); // is Call.cpp really necessary ? cant we optimize it?

	context->callStack.push_back(methodCall);

	lOffset += executingMethod->block->localesCount;

	locales->resize(locales->size() + targetMethod->block->localesCount);

	for (int i = 0; i < targetMethod->parametersCount; i++)
	{
		locales->at(lOffset + i) = context->PopStack();
	}

	ip = 0;
	ins = targetMethod->block->instructions;
}

void Exec::FreeHeap(std::vector<RuntimeContext::RuntimeElement>* locales, int& lOffset)
{
	for (int i = lOffset; i < locales->size(); i++) // manage this.
	{
		if (std::holds_alternative<std::string*>(locales->at(i)))
		{
			string* value = std::get<string*>(locales->at(i));
			delete value;
		}

	}
}

void Exec::DispatchNative(RuntimeContext* context, int& nativeType)
{
	switch (nativeType)
	{
	case Natives::Printl:
	{
		std::visit(overloaded
			{
					[](Null* arg) { std::cout << "null" << std::endl; },
					[](bool arg) { std::cout << (arg ? "true" : "false") << std::endl; },
					[](RuntimeStruct* arg) { std::cout << "{" << arg->typeClass->name << "}" << std::endl; },
					[](int arg) { std::cout << arg << std::endl; },
					[](std::string* arg) { std::cout << *arg << std::endl; },

			},
			context->PopStack());

		break;
	}
	case Natives::Readl:
	{
		string result;
		std::getline(std::cin, result);
		context->PushStack(&result);
		break;
	}
	}
}


