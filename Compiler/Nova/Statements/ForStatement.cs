using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class ForStatement : Statement, IParentBlock
    {
        public ForStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
        }

        public Class ParentClass => throw new NotImplementedException();

        IParentBlock IParentBlock.Parent => throw new NotImplementedException();

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
