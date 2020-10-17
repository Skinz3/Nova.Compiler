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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class ConstBoolStatement : Statement
    {
        private bool Value
        {
            get;
            set;
        }
        public ConstBoolStatement(IChild parent, bool value, ParserRuleContext context) : base(parent, context)
        {
            this.Value = value;
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
