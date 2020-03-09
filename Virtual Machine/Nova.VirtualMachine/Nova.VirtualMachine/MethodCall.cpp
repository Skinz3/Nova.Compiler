#include "Call.h"
#include "RuntimeContext.h"
#include "ByteMethod.h"

Call::Call(ByteMethod* method, ByteMethod* previousMethod, int returnIp, std::vector<RuntimeContext::RuntimeElement> previousLocales)
{
	this->method = method;
	this->returnIp = returnIp;
	this->previousMethod = previousMethod;
	this->previousLocales = previousLocales;
}
