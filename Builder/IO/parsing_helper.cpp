#include "parsing_helper.h"



vector<string> ParsingHelper::FindLinesUnderIndex(vector<string>* lines,int startLineIndex, int endLineIndex)
{
    vector<string> result;

    for (int i = startLineIndex; i < endLineIndex; i++)
    {
        result.push_back(lines->at(i));
    }
    return result;
}

int ParsingHelper::GetBracketCloseIndex(map<int,int>* brackets,int bracketOpenIndex)
{
    int openIndent = GetIndentLevel(brackets,bracketOpenIndex);
  
    map<int, int>::iterator current = brackets->begin();

    while (current != brackets->end())
    {
        if (current->first <= bracketOpenIndex)
        {
            current++;
        }
        else
        {
            if (current->second == openIndent-1)
            {
                return current->first;
            }
            current++;
        }
        
    }
    return -1;

}
int ParsingHelper::FindNextOpenBracket(vector<string>* lines,int lineIndex)
{
    for (int i = lineIndex;i < lines->size();i++)
    {
        string line = lines->at(i);

        if (line.find(BRACKET_START_DELIMITER) != string::npos)
        {
            return i;
        }
    }
    return -1;
}
int ParsingHelper::GetIndentLevel(map<int, int>* brackets,int lineIndex)
{
  

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
SearchResult ParsingHelper::SearchFirst(vector<string>* lines,string pattern, int index)
{
    SearchResult result;

    for (int i = 0; i < lines->size(); i++)
    {
        string line = lines->at(i);

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
vector<SearchResult> ParsingHelper::Search(vector<string>* lines,string pattern, int index)
{
    vector<SearchResult> results;

    for (int i = 0; i < lines->size(); i++)
    {
        string line = lines->at(i);

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