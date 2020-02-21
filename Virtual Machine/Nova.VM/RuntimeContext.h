#include <vector>
#include <variant>

#pragma once
class RuntimeContext
{
	
public:
	using StackElement = std::variant<int, float>;


	StackElement locales[255];
	void PushStack(StackElement element);
	StackElement PopStack();

private:
	std::vector<StackElement> stack;


};

