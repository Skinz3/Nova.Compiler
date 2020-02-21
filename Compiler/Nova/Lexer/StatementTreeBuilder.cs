using Nova.Lexer.Tokens;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Lexer
{
    public class StatementTreeBuilder
    {
        private const string REGEX_REMOVE_WHITESPACES = "\\s+(?=((\\[\\\"]|[^\\\"])*\"(\\[\\\"]|[^\\\"])*\")*(\\[\\\"]|[^\\\"])*$)";

        public static StatementNode Build(IParentBlock parent, string input, int lineIndex)
        {
            input = Regex.Replace(input, REGEX_REMOVE_WHITESPACES, string.Empty);

            Token[] tokens = Tokenizer.GenerateTokens(input);

            return Build(parent, tokens, lineIndex);
        }
        public static StatementNode Build(IParentBlock parent, Token[] tokens, int lineIndex)
        {
            tokens = Tokenizer.MergeBrackets(tokens);

            tokens = Tokenizer.MergeFunctions(tokens);

            tokens = Tokenizer.MergeVariableName(tokens);

            tokens = Tokenizer.MergeConstants(tokens);

            return MakeComponentTree(parent, lineIndex, tokens, 0, tokens.Length);
        }
        public static StatementNode MakeComponentTree(IParentBlock parent, int lineIndex, Token[] components, int start, int count)
        {
            if (components == null || count == 0) return new StatementNode(null);

            int idx = OperationOrder.FindLowestPart(components, start, count);
            if (idx == -1) return null;

            var pivot = components[idx];
            switch (pivot.Type)
            {
                case TokenType.OperatorBinary:
                    return MakeBinary(parent, lineIndex, components, start, count, idx);
                case TokenType.Expr:
                    return MakeFunction(parent, lineIndex, components[idx]);
                case TokenType.ConstantString:
                case TokenType.ConstantBoolean:
                case TokenType.ConstantInt:
                    return new StatementNode(parent, pivot.GetStatement(parent, lineIndex), null, null);

                case TokenType.Native:
                case TokenType.MethodCall:
                    return new StatementNode(parent, pivot.GetStatement(parent, lineIndex), null, null);
                case TokenType.Variable:
                    return new StatementNode(parent, pivot.GetStatement(parent, lineIndex), null, null);
            }

            throw new NotImplementedException(pivot.Type + " is not handled");

        }


        private static StatementNode MakeFunction(IParentBlock parent, int lineIndex, Token token)
        {
            var expr = token as ExpressionToken;
            return MakeComponentTree(parent, lineIndex, expr.Tokens, 0, expr.Tokens.Length);
        }

        private static StatementNode MakeBinary(IParentBlock parent, int lineIndex, Token[] tokens, int start, int count, int idx)
        {
            var leftin = MakeComponentTree(parent, lineIndex, tokens, start, idx - start);
            if (leftin == null) return null;

            var rightin = MakeComponentTree(parent, lineIndex, tokens, idx + 1, count - (idx - start) - 1);
            if (rightin == null) return null;

            return new StatementNode(parent, tokens[idx].GetStatement(parent, lineIndex), leftin, rightin);
        }
    }
}
