using Antlr4.Runtime;
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
    public abstract class Expression : ISemanticMember, IChild
    {
        public Class ParentClass => Parent.ParentClass;

        public IChild Parent
        {
            get;
            private set;
        }
        protected ParserRuleContext ParsingContext
        {
            get;
            private set;
        }
        public Expression(IChild parent,ParserRuleContext context)
        {
            this.Parent = parent;
            this.ParsingContext = context;
        }
        public abstract void GenerateBytecode(ClassesContainer container, ByteBlock context);

        public abstract void ValidateSemantics(SemanticsValidator validator);

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
