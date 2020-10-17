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

namespace Nova.Statements
{
    public class EmptyStatement : Statement
    {
        public EmptyStatement(IChild member, int lineIndex) : base(member, string.Empty, lineIndex)
        {

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
