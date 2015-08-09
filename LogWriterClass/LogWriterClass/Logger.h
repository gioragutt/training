#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <ctime>
#include <sstream>

typedef enum { INFO, WARNING, DEBUG } DebugLevel;
typedef const std::string& ConStrRef;

class Logger
{
public:
	Logger(const char* _fileName, ConStrRef _loggerName,
		   ConStrRef _token, DebugLevel _debugLevel);
	~Logger();
	void write(DebugLevel level, const std::string & message);
	void setDebugLevel(DebugLevel level);

protected:
	std::string getTimeStamp();
	std::string getDebugLevelName(DebugLevel lvl);

protected:
	std::ofstream _file;
	std::string _loggerName;
	std::string _token;
	DebugLevel _debugLevel;
};