#include "StoreStaticCode.h"

void StoreStaticCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	// todo 
	index++;
}

void StoreStaticCode::Deserialize(BinaryReader& reader)
{
	this->className = reader.ReadString();
	this->fieldName = reader.ReadString();
}
