#pragma once

#include <exception>
#include <string>

class file_not_found : public std::exception
{
public:
	file_not_found(const char* filePath);
	~file_not_found();
	virtual const char* what();
private:
	std::string _filePath;
};

