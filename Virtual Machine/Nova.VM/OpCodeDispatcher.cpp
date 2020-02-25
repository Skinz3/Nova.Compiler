#include "OpCodeDispatcher.h"
#include "ArithmeticCode.h"
#include "ComparaisonCode.h"
#include "JumpCode.h"
#include "JumpIfFalseCode.h"
#include "LoadCode.h"
#include "PrintlCode.h"
#include "PushConstCode.h"
#include "StoreCode.h"
#include "LoadStaticCode.h"
#include "LoadStaticMemberCode.h"
#include "MethodCallCode.h"
#include "MethodCallStaticCode.h"
#include "ReadlCode.h"
#include "ReturnCode.h"
#include "StoreStaticCode.h"
#include "StoreStaticMemberCode.h"

Code* OpCodeDispatcher::CreateCode(int id)
{
	switch (id)
	{
		case 1:
			return new ArithmeticCode();
		case 2:
			return new ComparaisonCode();
		case 3:
			return new JumpCode();
		case 4:
			return new JumpIfFalseCode();
		case 5:
			return new LoadCode();
		case 6:
			return new LoadStaticCode();
		case 7:
			return new LoadStaticMemberCode();
		case 8:
			return new MethodCallCode();
		case 9:
			return new MethodCallStaticCode();
		case 10:
			return new PrintlCode();
		case 11:
			return new PushConstCode();
		case 12:
			return new ReadlCode();
		case 13:
			return new ReturnCode();
		case 14:
			return new StoreCode();
		case 15:
			return new StoreStaticCode();
		case 16:
			return new  StoreStaticMemberCode();
		default:
			Logger::Error("Unable to create OpCode with typeId:" + std::to_string(id));
			return nullptr;
	}

	
}
