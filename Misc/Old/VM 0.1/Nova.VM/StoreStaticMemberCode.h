#pragma once
#include "Code.h"
class StoreStaticMemberCode :
	public Code
{
private:
	std::string fieldName;
public:
	void Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index);
	void Deserialize(BinaryReader& reader);
};

