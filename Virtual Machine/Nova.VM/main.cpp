#include <iostream>
#include "RuntimeContext.h"
#include "Code.h"
#include <ctime>
#include "BinaryReader.h"
#include "Exec.h"
#include "Logger.h"



int main(int argc, char* argv[])
{
	if (argc != 2)
	{
		Logger::Error("You need to specify one .nov file.");
		return EXIT_FAILURE;
	}

	std::string filename = argv[1];

	NovFile file;
	
	Logger::Log(filename);

	BinaryReader reader(filename);

	if (!file.Deserialize(reader))
	{
		reader.Close();
		return EXIT_FAILURE;
	}

	reader.Close();




	const clock_t begin_time = clock();

	

	std::cout << "Program terminated in: " << (float(clock() - begin_time) / CLOCKS_PER_SEC) * 1000 << "ms" << std::endl;


}
