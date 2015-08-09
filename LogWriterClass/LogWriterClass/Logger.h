#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <ctime>

class Logger
{
public:
	Logger(const char* _fileName, std::string _loggerName, std::string _tokene);
	~Logger();

protected:
	std::string getTimeStamp();

protected:
	std::ofstream _file;
	std::string _loggerName;
	std::string _token;
};

