#pragma once
#include "BinaryReader.h"
#include "ByteMethod.h"
#include "ByteField.h"
#include <vector>
#include "RuntimeContext.h"

class ByteClass
{
public:
	std::string name;
	std::vector<ByteMethod> methods;
	std::vector<ByteField> fields;
	std::vector<RuntimeContext::RuntimeElement> constants;
	void Deserialize(BinaryReader& reader);

	void Dispose();
};
