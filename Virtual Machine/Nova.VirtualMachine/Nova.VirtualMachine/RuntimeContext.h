#pragma once
#include <variant>
#include <string>
#include <vector>

class NovFile;

class RuntimeContext
{
public:
	using RuntimeElement = std::variant<int, std::string*>;

	RuntimeContext(NovFile* file);

	NovFile* novFile;

private:
	std::vector<RuntimeElement> stack;
};

