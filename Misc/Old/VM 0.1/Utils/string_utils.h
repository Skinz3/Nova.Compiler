#include <string>
#include <regex>

using namespace std;


class StringUtils
{
public:
    static string LeftTrim(string str);
    static string RightTrim(string str);
    static string Trim(string str);
    static bool IsEmptyOrWhiteSpace(string str);
    static vector<string> Split(const string& s, const char& c);
};