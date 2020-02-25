#include "LoadStaticMemberCode.h"

void LoadStaticMemberCode::Compute(RuntimeContext& context, RuntimeContext::StackElement locals[], int& index)
{
	// todo
}

void LoadStaticMemberCode::Deserialize(BinaryReader& reader)
{
	this->fieldName = reader.ReadString();
}
