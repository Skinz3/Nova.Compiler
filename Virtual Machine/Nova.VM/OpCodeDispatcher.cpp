#include "OpCodeDispatcher.h"

Code* OpCodeDispatcher::CreateCode(int id)
{
	switch (id)
	{
		case 1:
			break;
		case 2:
			break;
	}

	Logger::Error("Unable to create OpCode with typeId:" + id);
	return nullptr;
}
