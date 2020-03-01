using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class WhileStatement : Statement, IParentBlock
    {
        public const string REGEX = @"^while\s*\((.+)\)\s*({)?$";

        private StatementNode Condition
        {
            get;
            set;
        }
        private Statement[] Statements
        {
            get;
            set;
        }
        private int LineSize
        {
            get;
            set;
        }

        public Class ParentClass => this.Parent.ParentClass;

        IParentBlock IParentBlock.Parent => this.Parent;

        public WhileStatement(IParentBlock parent) : base(parent)
        {

        }

        public WhileStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            string conditionStr = match.Groups[1].Value;

            this.Condition = StatementTreeBuilder.Build(parent, conditionStr, lineIndex);

            int startIndex = Parser.FindNextOpenBracket(parent.ParentClass.File.Lines, lineIndex);
            int endIndex = Parser.GetBracketCloseIndex(parent.ParentClass.File.Brackets, startIndex);

            this.LineSize = (endIndex - startIndex) + 2;

            this.Statements = Parser.BuildStatementBlock(this, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray();


        }
        public override int GetLineSkip()
        {
            return LineSize;
        }

        public override void GenerateBytecode( ByteBlockMetadata context)
        {
            int jumpIndex = context.ByteCodeLength + 1;

            Condition.GenerateBytecode(context);

            JumpIfFalseCode jumpIfFalse = new JumpIfFalseCode(-1);

            context.Results.Add(jumpIfFalse);

            foreach (var statement in Statements)
            {
                statement.GenerateBytecode(context);
            }

            context.Results.Add(new JumpCode(jumpIndex));

            jumpIfFalse.targetIndex = context.ByteCodeLength + 1;

        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Condition.ValidateSemantics(validator);

            validator.BlockStart();

            foreach (var statement in Statements)
            {
                statement.ValidateSemantics(validator);
            }

            validator.BlockEnd();
        }

     
    }
}
