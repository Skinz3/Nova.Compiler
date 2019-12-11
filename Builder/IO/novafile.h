

#ifndef NVFILE
#define NVFILE


#include <string>
#include <vector>
#include <map>
#include "../Members/class.h"
#include <regex>
#include <algorithm>
#include <iostream>
#include <fstream>


using namespace std;

struct SearchResult
{
    int index;
    string value;
};

struct FileDefinition
{
    string _namespace;
    vector<string> usings;
};

class NovaFile
{

public:
    NovaFile(string fileName);
    string fileName;
    void Print();
    bool Read();
    bool ReadClasses();
    void Dispose();
    
private:
    FileDefinition definition;
    vector<string> *lines;
    bool ReadLines();
    bool ReadBrackets();

    vector<string> FindLinesUnderIndex(int startLineIndex, int endLineIndex);
    int GetIndentLevel(int lineIndex);
    int GetBracketCloseIndex(int bracketOpenIndex);
    int FindNextOpenBracket(int lineIndex);
    
    SearchResult SearchFirst(string pattern, int index);
    vector<SearchResult> Search(string pattern, int index);

    map<int,int>* brackets;

    vector<Class*>* classes;
};

#endif // NVFILE

