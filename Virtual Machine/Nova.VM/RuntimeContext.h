#pragma once

#include <vector>
#include <variant>
#include <string>
#include <iostream>
#include <string>

class NovFile;
class ByteMethod;
class ByteClass;

class RuntimeContext
{
public:
	NovFile* file;
	ByteClass* ExecutingClass;

	RuntimeContext(NovFile& file);
	using RuntimeElement = std::variant<int, std::string>;
	void PushStack(RuntimeContext::RuntimeElement& element);
	RuntimeContext::RuntimeElement PopStack();

	
	void Call(std::string className, std::string methodName, int paramsCount);
	void Call(std::string methodName, int paramsCount);

	void Initialize();

private:
	void Call(ByteMethod* method, int parametersCount);
	std::vector<RuntimeContext::RuntimeElement> stack;

};

