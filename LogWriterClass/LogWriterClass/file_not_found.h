#ifndef FILE_NOT_FOUND_H_
#define FILE_NOT_FOUND_H_

#include <exception>
#include <string>

class file_not_found : public std::exception
{
public:
	file_not_found();
	~file_not_found();
	virtual const char* what();
};

#endif /* FILE_NOT_FOUND_H_ */