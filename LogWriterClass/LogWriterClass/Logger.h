#ifndef LOGGER_H_
#define LOGGER_H_

#include <iostream>
#include <fstream>
#include <string>
#include <ctime>
#include <sstream>
#include "file_not_found.h"

typedef enum { INFO, WARNING, DEBUG } DebugLevel;
typedef const std::string& ConStrRef;

class Logger
{
public:
	Logger(const char* _fileName, ConStrRef _loggerName,
		   ConStrRef _token, DebugLevel _debugLevel);
	~Logger();

public: // Setters
	void setDebugLevel(DebugLevel _newLevel);
	void setToken(ConStrRef _newToken);
	void setName(ConStrRef _newLoggerName);

public: // Writer methods
	void write(DebugLevel level, ConStrRef message);
	void writeDebug(ConStrRef debugMessage);
	void writeWarning(ConStrRef warningMessage);
	void writeInfo(ConStrRef infoMessage);

protected: // Helping methods
	std::string getTimeStamp();
	std::string getDebugLevelName(DebugLevel lvl);

protected: // Data members
	std::ofstream _file;
	std::string _loggerName;
	std::string _token;
	DebugLevel _debugLevel;
};

#endif /* LOGGER_H_ */