#include "JumpCode.h"

void JumpCode::Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index)
{
	index = this->value;
}
void JumpCode::Deserialize(BinaryReader& reader)
{
	this->value = reader.Read<int>();
}
