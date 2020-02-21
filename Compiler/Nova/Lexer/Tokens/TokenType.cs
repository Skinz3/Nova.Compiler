using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Tokens
{
    public enum TokenType
    {
        ParenthesisOpen,
        ParenthesisClose,

        ConstantInt,
        ConstantBoolean,
        ConstantString,

        OperatorBinary,
        OperatorUnary,

        Expr,

        Variable,

        MethodCall,

        Dot,

        Comma,

        Native,

    }
}
