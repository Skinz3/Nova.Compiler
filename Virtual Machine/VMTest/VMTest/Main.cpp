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

	instructions.push_back(OpCodes::PUSH_STRING);

	instructions.push_back(OpCodes::PRINT_TOS);

	/*instructions.push_back(OpCodes::PUSH_INT);
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
	instructions.push_back(4); */


	cout << "Exec instructions." << endl;

	const clock_t begin_time = clock();

	RuntimeContext context;
	RuntimeContext::RuntimeElement locales[2];

	int ip = 0;

	while (ip < instructions.size()) // intructions contiens pushstring, printtos
	{
		switch (instructions[ip])
		{
		
		case OpCodes::PUSH_STRING:
		{
			string* value = new string();
			*value = "hello";
			context.PushString(value);
			ip++;
			break;
		}
		case OpCodes::PRINT_TOS:
		{
			std::string* ele = std::get<string*>(context.PopStack());// Ne fonctionne pas. 
			cout << *ele << endl;
			ip++;
		}

		}

	}
	std::cout << "Code executed in: " << (float(clock() - begin_time) / CLOCKS_PER_SEC) * 1000 << "ms" << std::endl;
	return 0;
}
