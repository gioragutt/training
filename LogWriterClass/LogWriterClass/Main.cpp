#include "Logger.h"
#include <iostream>
#include "file_not_found.h"
#include <iomanip>

int main()
{
	Logger log("c:/logfile.txt", "bug-logger", "giorag", DebugLevel::WARNING);

	DebugLevel lvl = DebugLevel::INFO;

	for (int i = 0; i < 40; i++)
	{
		log.write(lvl, "#" + std::to_string(INT_MAX - 150000 * i));
		for (int j = 0; j < INT_MAX / 10; j++)
		{ }
		lvl = (DebugLevel)((lvl + 1) % 3);
		if (i % 5 == 0)
			log.setDebugLevel(lvl);
	}

	return 0;
}

