#pragma once

#include <iostream>
#include "RuntimeContext.h"

class Code
{
	public:
		virtual void Compute(RuntimeContext& context, int& index) = 0;

};

