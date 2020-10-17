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
    public class ConstBoolExpression : Expression
    {
        private bool Value
        {
            get;
            set;
        }
        public ConstBoolExpression(IChild parent, ParserRuleContext context, bool value) : base(parent, context)
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
            throw new NotImplementedException();
        }
    }
}
