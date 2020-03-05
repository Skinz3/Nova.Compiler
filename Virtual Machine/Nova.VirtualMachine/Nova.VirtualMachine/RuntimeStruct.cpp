#include "RuntimeStruct.h"

RuntimeStruct::RuntimeStruct(ByteClass* typeClass)
{
	this->typeClass = typeClass;

	/* 
	   This loop is executed each time a struct is instantiated ~80ms for 1 million instantiation. 
	   think about a way to optimize it ? 
	 */
	for (ByteField* field : typeClass->fields) 
	{
		this->properties.push_back(field->value);
	} 
}

void RuntimeStruct::Set(int fieldId, RuntimeContext::RuntimeElement value)
{
	this->properties[fieldId] = value;
}

RuntimeContext::RuntimeElement RuntimeStruct::Get(int fieldId)
{
	return this->properties[fieldId];
}
