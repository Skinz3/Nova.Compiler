using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Members;
using Nova.Statements;

namespace Nova.Lexer.Tokens
{
    public class VariableNameToken : Token
    {
        public VariableNameToken(string raw) : base(raw)
        {

        }

        public override Statement GetStatement(IParentBlock parent, int lineIndex)
        {
            throw new NotImplementedException();
        }
    }
}
