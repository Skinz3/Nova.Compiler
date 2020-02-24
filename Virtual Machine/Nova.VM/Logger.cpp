#include "Logger.h"

void Logger::Log(std::string message)
{
	std::cout << message << std::endl;
}

void Logger::Error(std::string message)
{
	std::cout << "\033[1;31m" << message << "\033[0m\n" << std::endl;
}
