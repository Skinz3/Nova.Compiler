#pragma once
#include "Code.h"

class PushConstCode : public  Code
{
private:
	RuntimeContext::StackElement value;
public:
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
	void Deserialize(BinaryReader& reader);
};

