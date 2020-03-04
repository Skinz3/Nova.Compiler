#include "LoadCode.h"



void LoadCode::Compute(RuntimeContext &context,RuntimeContext::RuntimeElement* locales,int& index)
{
	context.PushStack(locales[variableId]);
	index++;
}
void LoadCode::Deserialize(BinaryReader& reader)
{
	this->variableId = reader.Read<int>();
}
