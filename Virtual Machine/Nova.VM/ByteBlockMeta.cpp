#include "ByteBlockMeta.h"
#include "OpCodeDispatcher.h"

void ByteBlockMeta::Deserialize(BinaryReader& reader)
{
	int localsRelatorCount = reader.Read<int>();


	for (int i = 0; i < localsRelatorCount; i++)
	{
		std::string key = reader.ReadString();
		int id = reader.Read<int>();
	}

	int codesCount = reader.Read<int>();

	for (int i = 0; i < codesCount; i++)
	{
		Code* code = OpCodeDispatcher::CreateCode(reader.Read<int>());

		if (code != nullptr) // return false, handle error
		{
			code->Deserialize(reader);
			this->Codes.push_back(code);
		}
	
	}
}
