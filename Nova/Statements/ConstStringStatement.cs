using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using Nova.Bytecode.Codes;

namespace Nova.Statements
{
    public class ConstStringStatement : Statement
    {
        public const string REGEX = "^\"(.+)\"$";

        private string Value
        {
            get;
            set;
        }
        public ConstStringStatement(IChild parent, string value, int lineIndex) : base(parent, string.Format("\"{0}\"", value), lineIndex)
        {
            this.Value = value;
        }
        public ConstStringStatement(IChild parent, string line, int lineIndex, Match match) : base(parent, line, lineIndex)
        {
            this.Value = match.Groups[1].Value;
        }
        public ConstStringStatement(IChild parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int constantId = context.BindConstant(Value);
            context.Instructions.Add(new PushConstCode(constantId));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {

        }
    }
}
