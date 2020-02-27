#include "PushConstCode.h"

void PushConstCode::Compute(RuntimeContext& context, RuntimeContext::RuntimeElement* locales, int& index)
{
	context.PushStack(value);
	index++;
}
void PushConstCode::Deserialize(BinaryReader& reader)
{
	byte type = reader.Read<byte>();

	switch (type)
	{
	case (byte)1:
		this->value = reader.Read<bool>();
		break;
	case (byte)2:
		this->value = reader.Read<int>();
		break;
	case (byte)3:
		this->value = reader.ReadString();
		break;
	}


}