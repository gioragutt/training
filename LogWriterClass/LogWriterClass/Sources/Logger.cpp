#include "../Headers/Logger.h"

using std::string;
using std::stringstream;

#pragma region Ctor Dtor VCATOI

// Initializes an instance of the logger class
// Parameters:
//	* _fileNamelog :	File for output
//	* _loggerName  :	Name of the logger
//	* _token	   :	Token in print
//	* _debugLevel  :	Debug level
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
		throw file_not_found();
}

// D'tor for logger
// Closes file if still open
Logger::~Logger()
{
	if (_file.is_open())
		_file.close();
}

#pragma endregion

#pragma region Setters

// Sets the debug level
void Logger::setDebugLevel(DebugLevel _newLevel)
{
	_debugLevel = _newLevel;
}

// Sets the token
void Logger::setToken(ConStrRef _newToken)
{
	_token = _newToken;
}

// Sets the name
void Logger::setName(ConStrRef _newLoggerName)
{
	_loggerName = _newLoggerName;
}

#pragma endregion

#pragma region Write Methods

// Writes message to log
// Parameters
//	* level	  :		Debug level (will print to screen if debug level is high enough)
//	* message :		Message to write to log
void Logger::write(DebugLevel level, ConStrRef message)
{
	stringstream print;
	
	// Get log print
	print << getDebugLevelName(level) << " [ " << getTimeStamp() << " ] ";
	if (_loggerName != "") { print << "[ " << _loggerName << " ] "; }
	if (_token != "") { print << "[ " << _token << " ] "; }
	print << ": " << message << std::endl;
	
	if (_file.is_open())
	{
		// Write log print and flush
		_file << print.str();
		_file.flush();
	}

	// If debug level is high enough, print to screen
	if (level <= _debugLevel)
		std::cout << print.str() << std::endl;
}

// Writes message in debug level
void Logger::writeDebug(ConStrRef debugMessage)
{
	write(DebugLevel::DEBUG, debugMessage);
}

// Writes message in warning level
void Logger::writeWarning(ConStrRef warningMessage)
{
	write(DebugLevel::WARNING, warningMessage);
}

// Writes message in info level
void Logger::writeInfo(ConStrRef infoMessage)
{
	write(DebugLevel::INFO, infoMessage);
}

#pragma endregion

// Gets the current time in format: DD/MM/YY HH:mm:SS
string Logger::getTimeStamp()
{
	// Variables for getting local time
	time_t currentTime;
	struct tm localTime;

	// Get time
	time(&currentTime);
	localtime_s(&localTime, &currentTime);

	// Get values of Date and Time
	int year = localTime.tm_year + 1900;
	int month = localTime.tm_mon + 1;
	int day = localTime.tm_mday;
	int hour = localTime.tm_hour;
	int minute = localTime.tm_min;
	int second = localTime.tm_sec;

	// Get time and date in correct format
	stringstream timeString;
	timeString << (day < 10 ? "0" : "") << day << "/" 
			   << (month < 10 ? "0" : "") << month << "/"
		       << year << " "
			   << (hour < 10 ? "0" : "") << hour << ":"
			   << (minute < 10 ? "0" : "") << minute << ":"
			   << (second < 10 ? "0" : "") << second;

	// Return formatted time and date
	return timeString.str();
}

// Get verbal for each debug level
string Logger::getDebugLevelName(DebugLevel level)
{
	switch (level)
	{
		case DebugLevel::WARNING:
			return "WARNING\t";
		case DebugLevel::INFO:
			return "INFO\t";
		case DebugLevel::DEBUG:
			return "DEBUG\t";
		default:
			return "WRONG-DEBUG-LEVEL";
	}
}
