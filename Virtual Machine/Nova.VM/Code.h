#pragma once

#include <iostream>
#include "RuntimeContext.h"
#include <vector>
#include "BinaryReader.h"

class Code
{
public:
	virtual void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index) = 0;
	virtual void Deserialize(BinaryReader& reader) = 0;

};

