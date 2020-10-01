#include <iostream>
#include "NovFile.h"
#include "Exec.h"
#include "Logger.h"
#include <ctime>

int main(int argc, char* argv[])
{
	
	if (argc != 2)
	{
		Logger::Log("You need to specify 1 .nov file");
		return 0;
	}
	std::string fileName = argv[1];

	Logger::Log(fileName);

	NovFile file(fileName);

	if (!file.Deserialize())
	{
		return EXIT_FAILURE;
	}

	// file.Print();

	const clock_t begin_time = clock();

	Exec::Run(file);

	std::cout << std::endl << "Code executed in: " << (float(clock() - begin_time) / CLOCKS_PER_SEC) * 1000 << "ms" << std::endl;

	// file.Dispose(); <-- get an error here. fix it

	return EXIT_SUCCESS;
}