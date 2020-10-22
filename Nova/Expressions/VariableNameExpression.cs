using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Symbols;
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
using Nova.Lexer;
using Nova.Bytecode.Enums;

namespace Nova.Expressions
{
    public class VariableNameExpression : Expression
    {
        public Expression AccessorExpression
        {
            get;
            set;
        }
        public List<Accessor> Tree
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            set;
        }
        public VariableNameExpression(IChild parent, ParserRuleContext context) : base(parent, context)
        {
            this.Tree = new List<Accessor>();
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            foreach (var element in Tree)
            {
                element.GenerateBytecode(container, context);
            }
        }

        public Accessor CreateAccessor()
        {
            return new Accessor(this);
        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            this.Tree = Accessor.BuildAccessorTree(this);

            Accessor.ValidateAccessorTree(this, validator);

        }
        public override string ToString()
        {
            return "Identifier :" + Name;
        }

    }
}
