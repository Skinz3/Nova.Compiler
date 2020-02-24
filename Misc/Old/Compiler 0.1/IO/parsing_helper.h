#include <string>
#include <vector>
#include <map>
#include <regex>

using namespace std;

const char BRACKET_START_DELIMITER = '{';
const char BRACKET_END_DELIMITER = '}';

struct SearchResult
{
    int index;
    string value;
};

class ParsingHelper
{
public:
    static vector<string> FindLinesUnderIndex(vector<string> *lines, int startLineIndex, int endLineIndex);
    static int GetIndentLevel(map<int, int> *brackets, int lineIndex);

    static SearchResult SearchFirst(vector<string> *lines, string pattern, int index);
    static vector<SearchResult> Search(vector<string> *lines, string pattern, int index);

    static int GetBracketCloseIndex(map<int, int> *brackets, int bracketOpenIndex);
    static int FindNextOpenBracket(vector<string> *lines, int lineIndex);

};