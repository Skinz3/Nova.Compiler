#include "MethodCall.h"

MethodCall::MethodCall(ByteClass* parentClass, std::vector<int> previousInstructions, int returnIp, std::vector<RuntimeContext::RuntimeElement> locales)
{
	this->previousInstructions = previousInstructions;
	this->returnIp = returnIp;
	this->locales = locales;
}
