#include "Logger.h"
#include <iostream>
#include "file_not_found.h"
#include <vector>

using std::vector;

int main()
{
	Logger logAllData("c:/logfile.txt", "logger name", "token", DebugLevel::DEBUG);
	Logger logNoToken("c:/logfile.txt", "logger name", "", DebugLevel::DEBUG);
	Logger logNoName("c:/logfile.txt", "", "token", DebugLevel::DEBUG);
	Logger logOnlyPath("c:/logfile.txt", "", "", DebugLevel::DEBUG);

	vector<Logger*> loggers;
	loggers.push_back(&logAllData);
	loggers.push_back(&logNoName);
	loggers.push_back(&logNoToken);
	loggers.push_back(&logOnlyPath);

	for (auto i = loggers.begin(), end = loggers.end(); i != end; ++i)
	{
		(*i)->write(DebugLevel::DEBUG, "Hello ke pasa amigo bag kok bag");
	}

	//for (int i = 0; i < 40; i++)
	//DebugLevel lvl = DebugLevel::INFO;
	//{
	//	log.write(lvl, "#" + std::to_string(INT_MAX - 150000 * i));
	//	for (int j = 0; j < INT_MAX / 10; j++)
	//	{ }
	//	lvl = (DebugLevel)((lvl + 1) % 3);
	//	if (i % 5 == 0)
	//		log.setDebugLevel(lvl);
	//}

	return 0;
}

