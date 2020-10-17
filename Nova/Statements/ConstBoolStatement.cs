using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
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
using Nova.Bytecode.Codes;

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
        public ConstBoolStatement(IChild parent, bool value, int lineIndex) : base(parent, string.Format("\"{0}\"", value), lineIndex)
        {
            this.Value = value;
        }
        public ConstBoolStatement(IChild parent, string line, int lineIndex, Match match) : base(parent, line.ToString(), lineIndex)
        {
           // this.Value = match.Groups[1].Value == Tokenizer.BOOLEAN_TRUE;
        }
        public ConstBoolStatement(IChild parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int variableId = context.BindConstant(Value);
            context.Instructions.Add(new PushConstCode(variableId)); 
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
        
        }
    }
}
