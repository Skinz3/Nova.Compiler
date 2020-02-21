#include <iostream>
#include "RuntimeContext.h"
#include "Code.h"
#include "BinaryAddCode.h"
#include "PushConstCode.h"
#include "StoreCode.h"
#include "LoadCode.h"
#include "ComparaisonCode.h"
#include "JumpCode.h"
#include "ComparaisonEnum.h"
#include "PrintCode.h"
#include <ctime>
#include "BinaryWriter.h"

void Run(std::vector<Code*>& codes, RuntimeContext& context)
{
	int index = 0;

	while (index < codes.size())
	{
		Code* element = codes[index];
		element->Compute(context, index);
	}
}

int main(int argc, char* argv[])
{
	BinaryWriter writer("C:/Users/Skinz/Desktop/file.bin");

	writer.WriteString("hello");

	writer.Close();

	
	RuntimeContext context;

	int indexVariable = 4;
	
	/* int i = 0
	   while (i < 1000000)
	   {
			i = i + 1
	   }
	   */

	std::vector<Code*> codes
	{
		 new PushConstCode(0), // i = 0
		 new StoreCode(indexVariable),
		 new LoadCode(indexVariable),
		 new PushConstCode(1000000),
		 new ComparaisonCode(ComparaisonEnum::Inferior,5),
		 new LoadCode(indexVariable),
		 new PushConstCode(1),
		 new BinaryAddCode(),
		 new StoreCode(indexVariable),
		 new JumpCode(2),
	};

	const clock_t begin_time = clock();

	Run(codes, context);

	std::cout << "Program terminated in: " << (float(clock() - begin_time) / CLOCKS_PER_SEC) * 1000 << "ms" << std::endl;




}
