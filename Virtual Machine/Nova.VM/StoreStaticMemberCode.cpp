#include "StoreStaticMemberCode.h"

void StoreStaticMemberCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	// todo
	index++;
}

void StoreStaticMemberCode::Deserialize(BinaryReader& reader)
{
	this->fieldName = reader.ReadString();
}
