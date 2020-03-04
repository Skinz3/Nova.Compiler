#pragma once
#include "Logger.h"
#include "Code.h"
class OpCodeDispatcher
{
public:
	static Code* CreateCode(int id);
};

