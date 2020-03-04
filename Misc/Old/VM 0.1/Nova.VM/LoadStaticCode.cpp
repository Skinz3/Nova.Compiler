#include "LoadStaticCode.h"

void LoadStaticCode::Compute(RuntimeContext& context,RuntimeContext::RuntimeElement* locales, int& index)
{

}

void LoadStaticCode::Deserialize(BinaryReader& reader)
{
	this->className = reader.ReadString();
	this->fieldName = reader.ReadString();
}
