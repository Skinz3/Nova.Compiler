using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
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
    public class ConstIntExpression : Expression
    {
        private int Value
        {
            get;
            set;
        }
        public ConstIntExpression(IChild parent, ParserRuleContext context, int value) : base(parent, context)
        {
            this.Value = value;
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            context.Instructions.Add(new PushIntCode(Value));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            
        }
    }
}
