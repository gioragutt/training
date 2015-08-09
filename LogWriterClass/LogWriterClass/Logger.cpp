#include "Logger.h"
#include "file_not_found.h"
#include <iomanip>

using std::string;
using std::stringstream;

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

// Writes message to log
// Parameters
//	* level	  :		debug level (will print if debug level is high enough
//	* message :		message to write to log
void Logger::write(DebugLevel level, const string & message)
{
	// Check debug level is high enough
	if (level <= _debugLevel)
	{
		_file << getDebugLevelName(level) << " [ " << getTimeStamp() << " ] [ " << _loggerName
	   		  << " ] [ " << _token << " ] : " << message << std::endl;
	}
}

void Logger::setDebugLevel(DebugLevel level)
{
	_debugLevel = level;
}

// D'tor for logger
// closes file if still open
Logger::~Logger()
{
	if (_file.is_open())
		_file.close();
}

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
			return "INFO\t";
		case DebugLevel::DEBUG:
			return "DEBUG\t";
		default:
			return "WRONG-DEBUG-LEVEL";
	}
}
