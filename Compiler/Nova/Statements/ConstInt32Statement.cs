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

namespace Nova.Statements
{
    public class ConstInt32Statement : Statement // its const Number statement.
    {
        public const string REGEX = @"^([+-])?([0-9]+)$";

        public int Value
        {
            get;
            private set;
        }
        public ConstInt32Statement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.Value = int.Parse(input);
        }
        public ConstInt32Statement(IParentBlock parent, int value) : base(parent)
        {
            this.Value = value;
        }
        public ConstInt32Statement(IParentBlock parent) : base(parent)
        {

        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            context.Results.Add(new PushIntCode(Value));
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