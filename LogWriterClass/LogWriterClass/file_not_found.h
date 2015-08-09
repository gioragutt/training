#ifndef FILE_NOT_FOUND_H_
#define FILE_NOT_FOUND_H_

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

#endif /* FILE_NOT_FOUND_H_ */