using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using Nova.Members;
using Nova.Statements;

namespace Nova.IO
{
    class Parser
    {
        public static int FindNextInstructionIndex(string[] lines, int startIndex)
        {
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int FindNextOpenBracket(string[] lines, int index)
        {
            for (int i = index; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.Contains(Constants.BRACKET_START_DELIMITER))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int GetBracketCloseIndex(Dictionary<int, int> brackets, int bracketOpenIndex)
        {
            int openIndent = GetIndentLevel(brackets, bracketOpenIndex);

            foreach (var bracket in brackets)
            {
                if (bracket.Key <= bracketOpenIndex)
                    continue;
                else if (bracket.Value == openIndent - 1)
                {
                    return bracket.Key;
                }
            }

            return -1;
        }
        static int GetIndentLevel(Dictionary<int, int> brackets, int lineIndex)
        {
            for (int i = 0; i < brackets.Count - 1; i++)
            {
                var bracket = brackets.ElementAt(i);
                var nextBracket = brackets.ElementAt(i + 1);

                if (lineIndex >= bracket.Key && lineIndex < nextBracket.Key)
                {
                    return bracket.Value;
                }
            }
            return 0;
        }
        public static List<Variable> ParseMethodDeclarationParameters(string parametersStr)
        {
            List<Variable> results = new List<Variable>();

            if (string.IsNullOrWhiteSpace(parametersStr))
            {
                return results;
            }

            string[] split = parametersStr.Split(',');

            foreach (var element in split)
            {
                string[] split2 = element.Trim().Split(' ');
                results.Add(new Variable(split2[1].Trim(), split2[0].Trim()));
            }
            return results;
        }
        public static List<Statement> BuildStatementBlock(IParentBlock parent, int startIndex, int endIndex, string[] lines)
        {
            List<Statement> statements = new List<Statement>();

            int i = startIndex;

            while (i < endIndex)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string line = lines[i];

                    Statement statement = StatementBuilder.Build(parent, lines[i], i);
                    statements.Add(statement);

                    i += statement.GetLineSkip();
                }
                else
                {
                    i++;
                }

            }
            return statements;
        }
        public static StatementNode[] ParseMethodCallParameters(IParentBlock parent, int lineIndex, string parametersStr)
        {
            List<StatementNode> results = new List<StatementNode>();

            if (string.IsNullOrWhiteSpace(parametersStr))
            {
                return results.ToArray();
            }
            else
            {
                foreach (var value in parametersStr.Split(','))
                {
                    results.Add(StatementTreeBuilder.Build(parent, value.Trim(), lineIndex));
                }
                return results.ToArray();
            }
        }
    }
}
