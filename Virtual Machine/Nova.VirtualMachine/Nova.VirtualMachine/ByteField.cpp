#include "ByteField.h"
#include "Exec.h"
#include "Logger.h"

void ByteField::Deserialize(BinaryReader& reader)
{
	this->name = reader.ReadString();
	this->modifiers = (Modifiers)reader.Read<char>();
	this->valueBlock = new ByteBlock();
	this->valueBlock->Deserialize(reader);
}

void ByteField::Dispose()
{
	delete valueBlock;
}

/*
	RuntimeContext.GetExecutingClass() will return null here. It is a problem?
*/
void ByteField::Initializer(RuntimeContext* context)
{
	/*std::vector<RuntimeContext::RuntimeElement> locales;
	Exec::Execute(context, locales, this->valueBlock->instructions);

	if (context->GetStackSize() == 1)
	{
		this->value = context->PopStack();
	}
	else
	{
		Logger::Error("Unable to initialize field " + this->name + " invalid value.");
	} */
}
