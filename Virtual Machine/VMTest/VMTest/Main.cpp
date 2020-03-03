#include "Main.h"
#include <vector>
#include "OpCodes.h"
#include "RuntimeContext.h"
#include <iostream>
#include <ctime>


using namespace std;

int main()
{
	vector<int> instructions;

	instructions.push_back(OpCodes::PUSH_INT);
	instructions.push_back(0);

	instructions.push_back(OpCodes::STORE);
	instructions.push_back(0);

	instructions.push_back(OpCodes::LOAD);
	instructions.push_back(0);

	instructions.push_back(OpCodes::PUSH_INT);
	instructions.push_back(1000000);

	instructions.push_back(OpCodes::CMP_INF);

	instructions.push_back(OpCodes::JUMP_IF_FALSE);
	instructions.push_back(99);

	instructions.push_back(OpCodes::LOAD);
	instructions.push_back(0);

	instructions.push_back(OpCodes::PUSH_INT);
	instructions.push_back(1);

	instructions.push_back(OpCodes::BINARY_ADD);

	instructions.push_back(OpCodes::STORE);
	instructions.push_back(0);

	instructions.push_back(OpCodes::JUMP);
	instructions.push_back(4);


	cout << "Exec instructions." << endl;

	const clock_t begin_time = clock();

	RuntimeContext context;
	RuntimeContext::RuntimeElement locales[2];

	int ip = 0;

	while (ip < instructions.size())
	{
		switch (instructions[ip])
		{
		case OpCodes::BINARY_ADD:
		{
			int val1 = context.PopStackInt();
			int val2 = context.PopStackInt();

			context.PushInt(val1 + val2);
			ip++;
			break;
		}
		case OpCodes::CMP_INF:
		{
			int val1 = context.PopStackInt();
			int val2 =  context.PopStackInt();
			context.PushInt(val2 < val1 ? 1 : 0);
			ip++;
			break;
		}
		case OpCodes::JUMP:
		{
			ip = instructions[++ip];
			break;
		}
		case OpCodes::LOAD:
		{
			context.PushStack(locales[instructions[++ip]]); // remove get, and get ms !
			ip++;
			break;
		}
		case OpCodes::JUMP_IF_FALSE:
		{
			int val1 = std::get<int>(context.PopStack());

			int target = instructions[++ip];

			if (val1 == 0)
			{
				ip = target;
			}
			else
			{
				ip++;
			}
			break;
		}
		case OpCodes::PUSH_INT: // this should be replaced by PushConst (and loaded from symbol table)
		{
			context.PushInt(instructions[++ip]);
			ip++;
			break;
		}
		case OpCodes::STORE:
		{
			locales[instructions[++ip]] = context.PopStack(); // remove get, and get ms !
			ip++;
			break;
		}

		}

	}
	std::cout << "Code executed in: " << (float(clock() - begin_time) / CLOCKS_PER_SEC) * 1000 << "ms" << std::endl;
	return 0;
}
