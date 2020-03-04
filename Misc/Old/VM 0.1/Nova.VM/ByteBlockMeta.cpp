#include "ByteBlockMeta.h"
#include "OpCodeDispatcher.h"

void ByteBlockMeta::Deserialize(BinaryReader& reader)
{
	this->localsCount = reader.Read<int>();

	for (int i = 0; i < localsCount; i++)
	{
		std::string key = reader.ReadString();
		int id = reader.Read<int>();
		string type = reader.ReadString();

		this->localsRelator.insert(std::make_pair(key, id));
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
		else
		{
			Logger::Error("Unable to deserialize code.");
		}
	
	}
}


