#include "StoreCode.h"


void StoreCode::Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index)
{
	locales[variableId] = context.PopStack();
	index++;
}
void StoreCode::Deserialize(BinaryReader& reader)
{
	this->variableId = reader.Read<int>();
}