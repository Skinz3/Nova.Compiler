#include "ByteClass.h"

void ByteClass::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();

	this->type = (ContainerType)reader.Read<char>();

	int methodsCount = reader.Read<int>();

	for (int i = 0; i < methodsCount; i++)
	{
		ByteMethod* method = new ByteMethod(this);
		method->Deserialize(reader);
		this->methods.push_back(method);
	}

	int fieldsCount = reader.Read<int>();

	for (int i = 0; i < fieldsCount; i++)
	{
		ByteField* field = new ByteField();
		field->Deserialize(reader);
		this->fields.push_back(field);
	}

	int constantsCount = reader.Read<int>();

	for (int i = 0; i < constantsCount; i++)
	{
		int type = reader.Read<int>();

		switch (type)
		{
			case 1:
			{
				string* value = new string(); // allocates a string.
				*value = reader.ReadString();
				constants.push_back(value);
				break;
			}
			case 2:
			{
				constants.push_back((bool)reader.Read<bool>());
				break;
			}
			default:
			{
				throw std::invalid_argument("Unknown constant type.");
			}
		}
	}
}

void ByteClass::Dispose()
{
	for (ByteMethod* method : this->methods)
	{
		method->Dispose();
		delete method;
	}
	for (ByteField* field : this->fields)
	{
		field->Dispose();
		delete field;
	}
	for (RuntimeContext::RuntimeElement element : this->constants)
	{
		delete& element;
	}
}
