using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
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
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Antlr4.Runtime;
using Nova.Expressions;

namespace Nova.Statements
{
    public class AssignationStatement : Statement
    {
        public VariableNameExpression Target
        {
            get;
            set;
        }

        public char Operator
        {
            get;
            set;
        }
        public ExpressionNode Value
        {
            get;
            set;
        }

        public AssignationStatement(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Value.GenerateBytecode(container, context);

            Target.GenerateBytecode(container, context);
           
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Value.ValidateSemantics(validator);

            Target.Store = true;
            Target.ValidateSemantics(validator);
            
        }
    }
}
