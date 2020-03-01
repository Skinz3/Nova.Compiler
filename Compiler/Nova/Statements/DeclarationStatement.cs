using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.Lexer.Tokens;
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
        public StatementNode Value
        {
            get;
            private set;
        }
        public DeclarationStatement(IParentBlock parent, string line, int lineIndex, Match match) : base(parent, line, lineIndex)
        {
            Variable = new Variable(match.Groups[2].Value, match.Groups[1].Value);
            this.Value = StatementTreeBuilder.Build(parent, match.Groups[4].Value, lineIndex);
        }
        public DeclarationStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            List<ICode> results = new List<ICode>();

            int variableId = context.SymbolTable.Bind(Variable.Name, Variable.Type);

            if (!Value.IsNull())
            {
                Value.GenerateBytecode(container,context);
                context.Results.Add(new StoreCode(variableId));
            }
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            validator.DeclareVariable(Variable.Name, Variable.Type);

            Value.ValidateSemantics(validator);
        }
    }
}
