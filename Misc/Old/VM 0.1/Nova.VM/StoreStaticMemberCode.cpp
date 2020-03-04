#include "StoreStaticMemberCode.h"

void StoreStaticMemberCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	// todo
	index++;
}

void StoreStaticMemberCode::Deserialize(BinaryReader& reader)
{
	this->fieldName = reader.ReadString();
}
