#include "MethodCallCode.h"

void MethodCallCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	// todo
}

void MethodCallCode::Deserialize(BinaryReader& reader)
{
	this->methodName = reader.ReadString();
	this->parametersCount = reader.Read<int>();
}
