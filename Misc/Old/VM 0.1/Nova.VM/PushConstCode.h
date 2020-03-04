#pragma once
#include "Code.h"

class PushConstCode : public  Code
{
private:
	RuntimeContext::RuntimeElement value;
public:
	void Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index);
	void Deserialize(BinaryReader& reader);
};

