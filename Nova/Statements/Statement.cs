using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Symbols;
using Nova.Bytecode.Enums;
using Antlr4.Runtime;

namespace Nova.Statements
{
    public abstract class Statement : ISemanticMember , IChild
    {
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

        public Class ParentClass => Parent.ParentClass;

        public Statement(IChild parent, ParserRuleContext ruleContext)
        {
            this.Parent = parent;
            this.ParsingContext = ruleContext;
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}", this.GetType().Name, ParsingContext.GetText());
        }

        public abstract void GenerateBytecode(ClassesContainer container, ByteBlock context);

        public abstract void ValidateSemantics(SemanticsValidator validator);



    }
}
