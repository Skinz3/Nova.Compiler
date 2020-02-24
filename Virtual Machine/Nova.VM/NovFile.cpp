#include "NovFile.h"
#include "Logger.h"


NovFile::NovFile()
{

}

bool NovFile::Deserialize(BinaryReader& reader)
{
	std::string header = reader.ReadString();

	if (header != NovFile::HEADER)
	{
		Logger::Error("Invalid file. Header corrupted.");
		return false;
	}



	return true;
}
