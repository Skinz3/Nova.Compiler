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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class ConstStringStatement : Statement
    {
        private string Value
        {
            get;
            set;
        }
        public ConstStringStatement(IChild parent, string value, ParserRuleContext context) : base(parent, context)
        {
            this.Value = value;
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
