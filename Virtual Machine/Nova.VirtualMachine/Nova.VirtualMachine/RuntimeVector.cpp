#include "RuntimeVector.h"

void RuntimeVector::Add(RuntimeContext::RuntimeElement element)
{
	this->elements.push_back(element);
}

RuntimeContext::RuntimeElement RuntimeVector::At(int index)
{
	return this->elements.at(index);
}

int RuntimeVector::Size()
{
	return this->elements.size();
}
