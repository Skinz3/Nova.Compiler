#include "LoadStaticMemberCode.h"

void LoadStaticMemberCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{
	// todo
}

void LoadStaticMemberCode::Deserialize(BinaryReader& reader)
{
	this->fieldName = reader.ReadString();
}
