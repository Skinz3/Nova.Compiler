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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class DeclarationStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\s*(=\s*(.*))?$";

        private Variable Variable
        {
            get;
            set;
        }
        public ExpressionNode Value
        {
            get;
            private set;
        }
        public DeclarationStatement(IChild parent, Variable variable, ExpressionNode value, ParserRuleContext context) : base(parent, context)
        {
            Variable = variable;
            this.Value = value;
        }


        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            List<ICode> results = new List<ICode>();

            int variableId = context.SymbolTable.Bind(Variable.Name, Variable.Type);

            Value.GenerateBytecode(container, context);
            context.Instructions.Add(new StoreCode(variableId));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            validator.DeclareVariable(Variable.Name, Variable.Type);

            Value.ValidateSemantics(validator);
        }
    }
}
