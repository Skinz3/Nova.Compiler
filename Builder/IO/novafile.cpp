#include <string>
#include "novafile.h"
#include <regex>
#include <string>
#include <algorithm>

using namespace std;

const string USING_PATTERN = "using (\\w.+)";
const string NAMESPACE_PATTERN = "namespace (\\w+)";
const string CLASS_PATTERN = "class (\\w+)";

const char BRACKET_START_DELIMITER = '{'; 
const char BRACKET_END_DELIMITER = '}';

NovaFile::NovaFile(string fileName)
{
    this->fileName = fileName;
}
bool NovaFile::Read()
{
    if (!ReadLines())
    {
        cout << "Unable to read file (IO error): " << this->fileName << endl;
        return false;
    }

    this->definition._namespace = SearchFirst(NAMESPACE_PATTERN, 1).value;

    if (this->definition._namespace == string())
    {
        cout << "Invalid file no namespace." << endl;
        return false;
    }

    for (SearchResult result : Search(USING_PATTERN,1))
    {
        this->definition.usings.push_back(result.value);
    }

    if (!ReadBrackets())
    {
        return false;
    }

    for (auto i : *brackets)
    {
        cout << i.first << ":" << i.second << endl;
    }

    return true;
}


bool NovaFile::ReadLines()
{
    ifstream fstream(fileName);

    if (!fstream.good())
    {
        return false;
    }

    this->lines = new vector<string>();

    string line;

    while (getline(fstream, line))
    {
        lines->push_back(line);
    }

    fstream.close();
    return true;
}
bool NovaFile::ReadBrackets()
{
    this->brackets = new map<int,int>();

    int currentIndent = 0;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        int count = std::count(line.begin(), line.end(),BRACKET_START_DELIMITER); 

        if (count > 0)
        {
            currentIndent+=count;
            brackets->insert(make_pair(i, currentIndent));
        }

      

        count = std::count(line.begin(), line.end(),BRACKET_END_DELIMITER); 

        if (count > 0)
        {
            currentIndent-=count;
            brackets->insert(make_pair(i, currentIndent));
        }
    }

    if (!brackets->empty())
    {
        int lastIndentLevel = (--brackets->end())->second;
        if (lastIndentLevel != 0)
        {

            cout << "Invalid file brackets. (Last bracket indent level: " << lastIndentLevel << ")" << endl;
            return false;
        }
    }
    return true;
}
bool NovaFile::ReadClasses() // Reflechir a un algorithme optimisé de parsing de classes.
{   
    this->classes = new vector<Class*>();

    vector<SearchResult> results = Search(CLASS_PATTERN, 1);

    if (results.size() == 0)
    {
        cout << "Invalid file, no classes." << endl;
        return false;
    } 
  
    for (SearchResult result : results)
    {
 
        string className  = result.value;
        int classStartLine = result.index;
      
        int classEndLine = GetBracketCloseIndex(classStartLine); 

        cout << "class " << className << "(" << classStartLine<< ":" << classEndLine <<")"<<endl;

        vector<string> classLines = FindLinesUnderIndent(classStartLine,classEndLine);

        Class* novaClass  = new Class(classLines);

        if (!novaClass->Build())
        {
            return false;
        }
        this->classes->push_back(novaClass);
    }

    return true;
}
vector<string> NovaFile::FindLinesUnderIndent(int startLineIndex, int endLineIndex)
{
    vector<string> result;

    for (int i = startLineIndex; i < endLineIndex; i++)
    {
        result.push_back(this->lines->at(i));
    }
    return result;
}
int NovaFile::GetBracketCloseIndex(int bracketOpenIndex)
{
    int openIndent = GetIndentLevel(bracketOpenIndex);
  

    map<int, int>::iterator current = brackets->begin();

    while (current != brackets->end())
    {
        if (current->first < bracketOpenIndex)
        {
            current++;
        }
        else
        {
            if (current->second == openIndent)
            {
                return current->first+1;
            }
            current++;
        }
        
    }
    return -1;

}
int NovaFile::GetIndentLevel(int lineIndex)
{
    if (lineIndex > (lines->size() - 1))
    {
        return -1;
    }

    map<int, int>::iterator current = brackets->begin();

    while (current != brackets->end())
    {
        int index1 = current->first;

        current++;

        int index2 = current->first;

        if (lineIndex >= index1 && lineIndex < index2)
        {
            current--;
            return current->second;
        }
    }
    return 0;
}
SearchResult NovaFile::SearchFirst(string pattern, int index)
{
    SearchResult result;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        regex r{pattern, regex_constants::ECMAScript};
        smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            result.index = i;
            result.value = match[index];
            return result;
        }
    }
    return result;
}
vector<SearchResult> NovaFile::Search(string pattern, int index)
{
    vector<SearchResult> results;

    for (int i = 0; i < this->lines->size(); i++)
    {
        string line = this->lines->at(i);

        regex r{pattern, regex_constants::ECMAScript};
        smatch match;

        regex_search(line, match, r);

        if (match.size() > 0)
        {
            SearchResult result;
            result.index = i;
            result.value = match[index];
            results.push_back(result);
        }
    }
    return results;
}
void NovaFile::Dispose()
{
    delete brackets;
    delete lines;

   
}
