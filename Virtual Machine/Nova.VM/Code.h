#pragma once

#include <iostream>
#include "RuntimeContext.h"
#include <vector>

class Code
{
public:
	virtual void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index) = 0;

};

