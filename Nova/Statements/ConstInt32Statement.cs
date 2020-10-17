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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class ConstInt32Statement : Statement // its const Number statement.
    {
        public int Value
        {
            get;
            private set;
        }
        public ConstInt32Statement(IChild parent, int value, ParserRuleContext context) : base(parent, context)
        {
            this.Value = value;
        }
       
        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            context.Instructions.Add(new PushIntCode(Value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {

        }
    }
}