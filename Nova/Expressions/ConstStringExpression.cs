using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions
{
    public class ConstStringExpression : Expression
    {
        private string Value
        {
            get;
            set;
        }
        public ConstStringExpression(IChild parent, ParserRuleContext context, string value) : base(parent, context)
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
