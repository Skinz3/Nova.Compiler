using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    class Tokenizer
    {
        public static char[] Numerics = new char[]
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        public static string BOOLEAN_TRUE = "true";

        public static string BOOLEAN_FALSE = "false";

        public static readonly string[] SortedOperators = new string[]
        {
            "|", "||", "&&", "==", "!=", "<", "<=", ">", ">=", "+", "-", "*", "^", "/", "%"
        };

        public const string CONSTANT_LITTERAL = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s*";

        public const string CTOR_KEYWORD = "->";

        public const string PARENTHESIS_OPEN = "(";

        public const string PARENTHESIS_CLOSE = ")";

        public const string NATIVE = "~";

        public static Token[] GenerateTokens(string input)
        {
            List<Token> tokens = new List<Token>();

            int index = 0;

            while (index < input.Length)
            {
                char chara = input[index];

                if (input[index] == PARENTHESIS_OPEN[0])
                {
                    tokens.Add(new BasicToken(PARENTHESIS_OPEN, TokenType.ParenthesisOpen));
                    index++;
                }
                else if (input[index] == PARENTHESIS_CLOSE[0])
                {
                    tokens.Add(new BasicToken(PARENTHESIS_CLOSE, TokenType.ParenthesisClose));
                    index++;
                }
                else if (index < input.Length - CTOR_KEYWORD.Length && CTOR_KEYWORD == input.Substring(index, CTOR_KEYWORD.Length))
                {
                    tokens.Add(new BasicToken(CTOR_KEYWORD, TokenType.Ctor));
                    index += 2;
                }
                else if (Numerics.Contains(input[index]))
                {
                    BasicToken numericToken = BasicToken.ParseNumericToken(input, ref index);
                    tokens.Add(numericToken);
                }
                else if (Array.Exists(SortedOperators, x => x.First() == input[index]))
                {
                    BasicToken operatorToken = BasicToken.ParseOperatorToken(tokens, input, ref index);
                    tokens.Add(operatorToken);
                }
                else if (input[index] == '\"')
                {
                    BasicToken litteralToken = BasicToken.ParseConstantStringToken(input, ref index);
                    tokens.Add(litteralToken);
                }
                else if (input[index] == '.')
                {
                    BasicToken dotToken = new BasicToken(input[index].ToString(), TokenType.Dot);
                    index++;
                    tokens.Add(dotToken);
                }
                else if (input[index] == ',')
                {
                    BasicToken dotToken = new BasicToken(input[index].ToString(), TokenType.Comma);
                    index++;
                    tokens.Add(dotToken);
                }
                else if (input[index] == NATIVE[0])
                {
                    BasicToken nativeToken = new BasicToken(NATIVE, TokenType.Native);
                    index++;
                    tokens.Add(nativeToken);
                }
                else if (char.IsLetter(input[index]))
                {
                    string sub = input.Substring(index, input.Length - index);

                    Match constantLitteralMatch = Regex.Match(sub, CONSTANT_LITTERAL);

                    if (constantLitteralMatch.Success)
                    {
                        string result = constantLitteralMatch.Groups[1].Value;

                        if (result == BOOLEAN_FALSE || result == BOOLEAN_TRUE)
                        {
                            tokens.Add(new BasicToken(result, TokenType.ConstantBoolean));
                        }
                        else
                        {
                            tokens.Add(new BasicToken(result, TokenType.Variable));

                        }

                        index += result.Length;
                    }
                }
                else
                {
                    throw new Exception("Unrecognized token  :" + input.Substring(index, input.Length - index));
                }
            }

            return tokens.ToArray();

        }
        public static Token[] MergeVariableName(Token[] tokens)
        {
            if (tokens.Length <= 1)
            {
                return tokens;
            }

            List<Token> results = new List<Token>();

            int i = 0;

            while (i < tokens.Length - 1)
            {
                Token current = tokens[i];

                Token next = tokens[i + 1];

                if (current.Type == TokenType.Variable && next.Type == TokenType.Dot)
                {
                    string variableName = string.Empty;

                    while (i < tokens.Length)
                    {
                        if (current.Type == TokenType.Variable || current.Type == TokenType.Dot)
                        {
                            variableName += current.Raw;
                            i++;

                            if (i < tokens.Length)
                                current = tokens[i];
                        }
                        else
                        {
                            break;
                        }

                    }
                    BasicToken variable = new BasicToken(variableName, TokenType.Variable);
                    results.Add(variable);
                }
                else
                {
                    results.Add(current);
                    i++;

                    if (i == tokens.Length - 1)
                        results.Add(next);
                }
            }

            return results.ToArray();
        }
        public static Token[] MergeFunctions(Token[] tokens)
        {
            if (tokens.Length <= 1)
            {
                return tokens;
            }

            List<Token> results = new List<Token>();

            int i = 0;

            while (i < tokens.Length - 1)
            {
                Token current = tokens[i];

                Token next = tokens[i + 1];

                if (current.Type == TokenType.Variable && next.Type == TokenType.Expr)
                {
                    string methodName = string.Empty;

                    int j = i + 1;

                    while (j > 0)
                    {
                        j--;
                        Token token = tokens[j];

                        if (token.Type == TokenType.Dot || token.Type == TokenType.Variable)
                        {
                            methodName = token.Raw + methodName;
                            results.Remove(token);

                        }
                        else
                        {
                            break;
                        }
                    }

                    TokenType type = TokenType.MethodCall;

                    if (i - 1 >= 0 && tokens[i - 1].Type == TokenType.Native)
                    {
                        type = TokenType.Native;
                    }
                    if (i - 1 >= 0 && tokens[i - 1].Type == TokenType.Ctor)
                    {
                        type = TokenType.Ctor;
                    }

                    MethodCallToken methodCall = new MethodCallToken(type, methodName + next.Raw, methodName, (ExpressionToken)next);
                    results.Add(methodCall);
                    i += 2;
                }
                else
                {
                    if (current.Type != TokenType.Native && current.Type != TokenType.Ctor)
                        results.Add(current);
                    i++;

                    if (i == tokens.Length - 1 && next.Type != TokenType.Native)
                        results.Add(next);
                }
            }

            return results.ToArray();
        }

        public static Token[] MergeConstants(Token[] tokens)
        {
            return tokens;
        }

        private static Token GetExpression(Token[] tokens, ref int index)
        {
            var tmp = new List<Token>();
            int jj = index + 1;
            while (jj < tokens.Length)
            {
                var cur = tokens[jj];

                switch (cur.Type)
                {
                    case TokenType.ParenthesisOpen:
                        tmp.Add(GetExpression(tokens, ref jj));
                        continue;
                    case TokenType.ParenthesisClose:
                        index = jj + 1;
                        return new ExpressionToken(tmp.ToArray());
                    default:
                        tmp.Add(cur);
                        jj++;
                        break;
                }
            }

            return null;
        }
        public static Token[] MergeBrackets(Token[] Comps)
        {
            var output = new List<Token>();
            int index = 0;

            while (index < Comps.Length)
            {
                var cur = Comps[index];

                if (cur.Type == TokenType.ParenthesisOpen)
                {
                    Token newToken = GetExpression(Comps, ref index);
                    output.Add(newToken);
                }
                else
                {
                    output.Add(cur);
                    index++;
                }
            }
            return output.ToArray();

        }

    }
}
