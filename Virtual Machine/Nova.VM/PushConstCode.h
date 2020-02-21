#pragma once
#include "Code.h"

class PushConstCode : public  Code
{
private:
	RuntimeContext::StackElement value;
public:
	PushConstCode(RuntimeContext::StackElement value);
	void Compute(RuntimeContext& context, int& index);
};

