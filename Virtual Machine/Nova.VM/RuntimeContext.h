#include <vector>
#include <variant>
#include <string>

#pragma once
class RuntimeContext
{
public:
	using StackElement = std::variant<int, bool, char*>;
	void PushStack(RuntimeContext::StackElement element);
	RuntimeContext::StackElement PopStack();
private:
	std::vector<RuntimeContext::StackElement> stack;

};

