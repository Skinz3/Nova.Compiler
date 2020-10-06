using Nova.Members;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    public class BasicToken : Token
    {
        private string Value
        {
            get;
            set;
        }
        public BasicToken(string value, TokenType type) : base(value)
        {
            this.Value = value;
            this.Type = type;
        }
        public override string ToString()
        {
            return "BasicToken (" + Type + ") " + this.Value;
        }
        public override Statement GetStatement(IParentBlock parent, int lineIndex)
        {
            switch (Type)
            {
                case TokenType.ParenthesisOpen:
                    throw new Exception();
                case TokenType.ParenthesisClose:
                    throw new Exception();
                case TokenType.ConstantInt: // distinguate int, long etc...
                    return new ConstInt32Statement(parent, int.Parse(Value));
                case TokenType.ConstantString:
                    return new ConstStringStatement(parent, Value, lineIndex);
                case TokenType.ConstantBoolean:
                    return new ConstBoolStatement(parent, Value == Tokenizer.BOOLEAN_TRUE, lineIndex);
                case TokenType.OperatorBinary:
                    return new OperatorStatement(parent, Value, lineIndex);
                case TokenType.Variable:
                    return new VariableNameStatement(parent, Value, lineIndex);
                case TokenType.Expr:
                    throw new Exception();
                default:
                    break;
            }
            throw new Exception(Type + " is not handled");
        }

        /// <summary>
        /// We also need to handle comma. ','
        /// </summary>
        public static BasicToken ParseNumericToken(string input, ref int index)
        {
            string result = "";

            while (index < input.Length && Tokenizer.Numerics.Contains(input[index]))
            {
                if (char.IsNumber(input[index]))
                {
                    result += input[index];
                    index++;
                }
                else
                {
                    throw new Exception("The numeric value is inccorect.");
                }
            }

            return new BasicToken(result, TokenType.ConstantInt);
        }

        public static BasicToken ParseConstantStringToken(string input, ref int index)
        {
            string result = "";

            index++;

            while (index < input.Length)
            {
                if (input[index] == '\"')
                {
                    index++;
                    break;
                }
                result += input[index];
                index++;
            }

            return new BasicToken(result, TokenType.ConstantString);
        }
        /// <summary>
        /// Tokens can be more than one character (==)
        /// </summary>
        public static BasicToken ParseOperatorToken(IEnumerable<Token> tokens, string input, ref int index)
        {
            int i = index;

            var op = Tokenizer.SortedOperators.First(x => x.FirstOrDefault() == input[i]);

            TokenType type = TokenType.OperatorBinary;

            switch (input[index])
            {
                case '+':
                case '-':
                  /*  if (tokens.Count() == 0 || tokens.Last().IsUnary())
                        type = TokenType.OperatorUnary; */
                    break;
            }


            BasicToken token = new BasicToken(op, type);
            index += op.Length;
            return token;
        }
    }
}
