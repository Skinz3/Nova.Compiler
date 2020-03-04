#include "Logger.h"

void Logger::Log(std::string message)
{
	std::cout << message << std::endl;
}

void Logger::Debug(std::string message)
{
	std::cout << "\033[1;30m" << message << "\033[0m" << std::endl;
}

void Logger::Error(std::string message)
{
	std::cout << "\033[1;31m" << message << "\033[0m" << std::endl;
}