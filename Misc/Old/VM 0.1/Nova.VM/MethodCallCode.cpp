#include "MethodCallCode.h"

void MethodCallCode::Compute(RuntimeContext& context, RuntimeContext::RuntimeElement* locales, int& index)
{
	context.Call(methodName, parametersCount);
	index++;
}

void MethodCallCode::Deserialize(BinaryReader& reader)
{
	this->methodName = reader.ReadString();
	this->parametersCount = reader.Read<int>();
}
