using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Members;
using Nova.Statements;

namespace Nova.Lexer.Tokens
{
    public class MethodCallToken : Token
    {
        private string MethodName
        {
            get;
            set;
        }
        private List<Token[]> Parameters
        {
            get;
            set;
        }
        public MethodCallToken(TokenType type, string raw, string methodName, ExpressionToken parameters) : base(raw)
        {
            this.Type = type;
            this.MethodName = methodName;
            this.Parameters = SplitParameters(parameters);
        }

        public override Statement GetStatement(IParentBlock member, int lineIndex)
        {
            List<StatementNode> parametersNodes = new List<StatementNode>();

            foreach (var parameter in Parameters)
            {
                parametersNodes.Add(StatementTreeBuilder.Build(member, parameter, lineIndex));
            }

            if (Type == TokenType.MethodCall)
            {
                return new MethodCallStatement(member, Raw, lineIndex, MethodName, parametersNodes.ToArray());
            }
            else if (Type == TokenType.Native)
            {
                return new NativeStatement(member, Raw, lineIndex, MethodName, parametersNodes.ToArray());
            }
            else
            {
                throw new Exception("Not handled.");
            }
        }

        private static List<Token[]> SplitParameters(ExpressionToken parameters)
        {
            List<Token[]> result = new List<Token[]>();

            int i = 0;

            List<Token> current = new List<Token>();

            while (i < parameters.Tokens.Length)
            {
                var token = parameters.Tokens[i];

                if (token.Type == TokenType.Comma)
                {
                    result.Add(current.ToArray());
                    current.Clear();
                }
                else
                {
                    current.Add(parameters.Tokens[i]);

                    if (i == parameters.Tokens.Length - 1)
                    {
                        result.Add(current.ToArray());
                    }
                }
                i++;
            }

            return result;
        }
    }
}
