#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();

	int methodCount = reader.Read<int>();

	for (int i = 0; i < methodCount; i++)
	{
		ByteMethod method(this);
		method.Deserialize(reader);
		this->methods.push_back(method);
	}

	int fieldsCount = reader.Read<int>();

	for (int i = 0; i < fieldsCount; i++)
	{
		ByteField field;
		field.Deserialize(reader);
		this->fields.push_back(field);
	}

	int constantsCount = reader.Read<int>();

	for (int i = 0; i < constantsCount; i++)
	{
		int type = reader.Read<int>();

		if (type == 1)
		{
			string* value = new string(); // allocates a string.
			*value = reader.ReadString();
			constants.push_back(value);
		}
		else if (type == 2)
		{
			constants.push_back((bool)reader.Read<bool>());
		}
	}
}

void ByteClass::Dispose()
{
	for (RuntimeContext::RuntimeElement element : this->constants)
	{
		delete& element;
	}
}
