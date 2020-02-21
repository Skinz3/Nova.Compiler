#pragma once
#include "Code.h"
#include <string>

class PrintCode :
	public Code
{
private:
	std::string value;
public:
	PrintCode(std::string value);
	void Compute(RuntimeContext& context, int& index);
};

