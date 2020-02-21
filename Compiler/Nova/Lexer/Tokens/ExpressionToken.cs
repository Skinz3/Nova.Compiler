using Nova.Members;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    public class ExpressionToken : Token
    {
        public override TokenType Type
        {
            get
            {
                return TokenType.Expr;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public Token[] Tokens
        {
            get;
            set;
        }
        public ExpressionToken(Token[] tokens) : base(string.Empty)
        {
            this.Tokens = tokens;
        }

        public override Statement GetStatement(IParentBlock member,int lineIndex)
        {
            throw new NotImplementedException();
        }
    }
}
