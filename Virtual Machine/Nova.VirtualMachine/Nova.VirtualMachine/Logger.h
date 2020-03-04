#pragma once
#include <string>
#include <iostream>
class Logger
{
public:
	static void Log(std::string message);
	static void Debug(std::string message);
	static void Error(std::string message);
};