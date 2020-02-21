using Nova.Members;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    public abstract class Token
    {
        public virtual TokenType Type
        {
            get;
            set;
        }
        public string Raw
        {
            get;
            private set;
        }
        public Token(string raw)
        {
            this.Raw = raw;
        }

        public abstract Statement GetStatement(IParentBlock parent,int lineIndex);

        public bool IsUnary()
        {
            switch (Type)
            {
                case TokenType.ParenthesisOpen:
                case TokenType.ParenthesisClose:
                case TokenType.OperatorBinary:
                case TokenType.OperatorUnary:
                    return true;
                default:
                    return false;
            }
        }
    }
}
