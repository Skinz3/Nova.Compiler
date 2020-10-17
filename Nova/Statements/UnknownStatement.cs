using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class UnknownStatement : Statement
    {
        public UnknownStatement(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }


        public override string ToString()
        {
            return "Unknown Statement (" + this.ParsingContext.GetText() + ")";
        }



        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            throw new NotImplementedException();
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            throw new NotImplementedException();
        }
    }
}
