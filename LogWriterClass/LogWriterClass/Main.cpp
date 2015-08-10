#include "Logger.h"
#include <iostream>
#include "file_not_found.h"
#include <vector>

using std::vector;
using std::cout;
using std::endl;

int main()
{
	Logger* log = nullptr;

	try
	{
		log = new Logger("c:/logfile.txt", "ZOHER-LOGGER #1", "Token #1", DebugLevel::INFO);
	}
	catch (file_not_found& exp)
	{
		cout << exp.what() << endl;
	}

	if (log == nullptr)
		return 0;

	cout << "Log level is INFO, should print once to cout and three times to file" << endl;
	cout << "--------------------------------------------------------------------" << endl;
	log->writeInfo("Log level is info, and this is info print");
	log->writeWarning("Log level is info, and this is warning print");
	log->writeDebug("Log level is info, and this is debug print");
	cout << "--------------------------------------------------------------------" << endl << endl;

	cout << "Log level is WARNING, should print twice to cout and three times to file" << endl;
	cout << "--------------------------------------------------------------------" << endl;

	log->setDebugLevel(DebugLevel::WARNING); log->setName("ZOHER-LOGGER #2"); log->setToken("Token #2");

	log->writeInfo("Log level is warning, and this is info print");
	log->writeWarning("Log level is warning, and this is warning print");
	log->writeDebug("Log level is warning, and this is debug print");
	cout << "--------------------------------------------------------------------" << endl << endl;

	cout << "Log level is DEBUG, should print three times to cout and three times to file" << endl;
	cout << "--------------------------------------------------------------------" << endl;

	log->setDebugLevel(DebugLevel::DEBUG); log->setName("ZOHER-LOGGER #3"); log->setToken("Token #3");
	
	log->writeInfo("Log level is debug, and this is info print");
	log->writeWarning("Log level is debug, and this is warning print");
	log->writeDebug("Log level is debug, and this is debug print");
	cout << "--------------------------------------------------------------------" << endl;

	return 0;
}

