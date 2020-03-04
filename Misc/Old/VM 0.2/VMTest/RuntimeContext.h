#pragma once
#include <variant>
#include <vector>
#include <string>

class RuntimeContext
{
public:
	using RuntimeElement = std::variant<int, std::string*>;

	void PushInt(int value);
	void PushStack(RuntimeContext::RuntimeElement & value);

	void PushString(std::string * value);

	int PopStackInt();

	RuntimeContext::RuntimeElement PopStack();
 

private:
	std::vector<RuntimeElement> stack;

};
