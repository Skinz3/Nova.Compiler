#include "MethodCallStaticCode.h"

void MethodCallStaticCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	// todo
}

void MethodCallStaticCode::Deserialize(BinaryReader& reader)
{
	this->className = reader.ReadString();
	this->methodName = reader.ReadString();
	this->parametersCount = reader.Read<int>();
}
