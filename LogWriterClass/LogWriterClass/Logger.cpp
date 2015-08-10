#include "Logger.h"
#include "file_not_found.h"
#include <iomanip>

using std::string;
using std::stringstream;

#pragma region C'tor D'tor VCATOI

// Initializes an instance of the logger class
// Parameters:
//	* _fileNamelog :	file for output
//	* _loggerName  :	name of the logger
//	* _token	   :	token in print
//	* _debugLevel  :	debug level
// Throws:
//	* file_not_found exception when file doesn't exist
Logger::Logger(const char* _fileName, ConStrRef _loggerName, ConStrRef _token, DebugLevel _debugLevel)
	: _loggerName(_loggerName)
	, _token(_token)
	, _debugLevel(_debugLevel)
{
	// try to open file
	_file.open(_fileName, std::ios::out | std::ios::app);

	// if file won't open, throw exception
	if (!_file.is_open())
		throw file_not_found(_fileName);
}

// D'tor for logger
// closes file if still open
Logger::~Logger()
{
	if (_file.is_open())
		_file.close();
}

#pragma endregion

#pragma region Setters

// sets the debug level of the logger
void Logger::setDebugLevel(DebugLevel _newLevel)
{
	_debugLevel = _newLevel;
}

// sets the token
void Logger::setToken(ConStrRef _newToken)
{
	_token = _newToken;
}

// sets the name
void Logger::setName(ConStrRef _newLoggerName)
{
	_loggerName = _newLoggerName;
}

#pragma endregion

#pragma region Write Methods

// Writes message to log
// Parameters
//	* level	  :		debug level (will print if debug level is high enough
//	* message :		message to write to log
void Logger::write(DebugLevel level, ConStrRef message)
{
	// Check debug level is high enough
	if (level <= _debugLevel)
	{
		_file << getDebugLevelName(level) << " [ " << getTimeStamp() << " ] ";
		if (_loggerName != "") { _file << "[ " << _loggerName << " ] "; }
		if (_token != "") { _file << "[ " << _token << " ] "; }
		_file << ": " << message << std::endl;
	}
}

void Logger::writeDebug(ConStrRef debugMessage)
{
	write(DebugLevel::DEBUG, debugMessage);
}

void Logger::writeWarning(ConStrRef warningMessage)
{
	write(DebugLevel::WARNING, warningMessage);
}

void Logger::writeInfo(ConStrRef infoMessage)
{
	write(DebugLevel::INFO, infoMessage);
}

#pragma endregion

// gets the current time in format: DD/MM/YY HH:mm:SS
string Logger::getTimeStamp()
{
	// variables for getting local time
	time_t currentTime;
	struct tm localTime;

	/* get time */
	time(&currentTime);
	localtime_s(&localTime, &currentTime);

	// get values of time and date
	int day  = localTime.tm_mday, month  = localTime.tm_mon + 1	, year	 = localTime.tm_year + 1900,
		hour = localTime.tm_hour, minute = localTime.tm_min		, second = localTime.tm_sec;

	// get time and date in correct format
	stringstream timeString;
	timeString << (day < 10 ? "0" : "") << day << "/" 
			   << (month < 10 ? "0" : "") << month << "/"
		       << year << " "
			   << (hour < 10 ? "0" : "") << hour << ":"
			   << (minute < 10 ? "0" : "") << minute << ":"
			   << (second < 10 ? "0" : "") << second;

	// return formatted time and date
	return timeString.str();
}

// get verbal for each debug level
string Logger::getDebugLevelName(DebugLevel lvl)
{
	switch (lvl)
	{
		case DebugLevel::WARNING:
			return "WARNING";
		case DebugLevel::INFO:
			return "INFO";
		case DebugLevel::DEBUG:
			return "DEBUG";
		default:
			return "WRONG-DEBUG-LEVEL";
	}
}
