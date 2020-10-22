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
using Nova.Expressions.Accessors;

namespace Nova.Expressions
{
    public class VariableNameExpression : AccessorExpression
    {
        public override AccessorType AccessorType => AccessorType.Field;

        public AccessorTree Tree
        {
            get;
            private set;
        }
        public bool Store
        {
            get;
            set;
        }


        public VariableNameExpression(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Tree.GenerateBytecode(container, context);
        }

      
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            this.Tree = new AccessorTree(this, Store);
            this.Tree.ValidateSemantics(validator);
        }
        public override string ToString()
        {
            return base.ToString() + " {" + Name + "}";
        }


    }
}
