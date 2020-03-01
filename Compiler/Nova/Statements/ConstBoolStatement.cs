using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer.Tokens;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class ConstBoolStatement : Statement
    {
        public const string REGEX = "^(true|false)$";

        private bool Value
        {
            get;
            set;
        }
        public ConstBoolStatement(IParentBlock parent, bool value, int lineIndex) : base(parent, string.Format("\"{0}\"", value), lineIndex)
        {
            this.Value = value;
        }
        public ConstBoolStatement(IParentBlock parent, string line, int lineIndex, Match match) : base(parent, line.ToString(), lineIndex)
        {
            this.Value = match.Groups[1].Value == Tokenizer.BOOLEAN_TRUE;
        }
        public ConstBoolStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            context.Results.Add(new CreateConstCode(Value));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            throw new NotImplementedException();
        }
    }
}
