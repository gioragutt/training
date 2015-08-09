#include "file_not_found.h"

// Initialize file_not_found exception with the file name
file_not_found::file_not_found(const char * filePath)
: _filePath(filePath)
{ }

file_not_found::~file_not_found()
{ }

const char * file_not_found::what()
{
	std::string message = "File \"" + _filePath + "\" not found!";
	return message.c_str();
}
