#include "RuntimeStruct.h"

void RuntimeStruct::Set(int fieldId, RuntimeContext::RuntimeElement& value)
{
	this->properties[fieldId] = value;
}

RuntimeContext::RuntimeElement RuntimeStruct::Get(int fieldId)
{
	return this->properties[fieldId];
}
