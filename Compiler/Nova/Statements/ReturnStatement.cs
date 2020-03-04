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

namespace Nova.Statements
{
    public class ReturnStatement : Statement
    {
        public const string REGEX = @"^return\s*(.+)?$";

        private StatementNode Value
        {
            get;
            set;
        }
        public ReturnStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            string valueStr = match.Groups[1].Value;
            Value = StatementTreeBuilder.Build(parent, valueStr, lineIndex); //  will return null if valueStr is empty or whitespace
        }
        public ReturnStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            Value.GenerateBytecode(container,context);
            context.Results.Add(new ReturnCode());
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Value.ValidateSemantics(validator);
        }
    }
}
