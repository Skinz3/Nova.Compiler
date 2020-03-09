#include "Call.h"
#include "RuntimeContext.h"
#include "ByteMethod.h"

Call::Call(ByteMethod* method, ByteMethod* previousMethod, int returnIp)
{
	this->method = method;
	this->returnIp = returnIp;
	this->previousMethod = previousMethod;
}
