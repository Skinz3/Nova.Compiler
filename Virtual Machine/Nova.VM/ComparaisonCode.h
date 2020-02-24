#pragma once
#include "Code.h"
#include "ComparaisonEnum.h"

class ComparaisonCode :
	public Code
{
	private:
		ComparaisonEnum type;
		int skip;
public:
	ComparaisonCode(ComparaisonEnum type,int skip);
	void Compute(RuntimeContext &context,RuntimeContext::StackElement locals[], int& index);
};

