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
    public class IfStatement : Statement
    {
        public const string IF_REGEX = @"^if\s*\((.+)\)\s*({)?$";

        public const string ELSE_REGEX = @"^else(\s*if?\s*)?(\((.*)\))?\s*({)?$";

        private int LineSize
        {
            get;
            set;
        }
        private StatementNode IfCondition
        {
            get;
            set;
        }
        private Statement[] IfStatements
        {
            get;
            set;
        }
        private StatementNode ElseCondition
        {
            get;
            set;
        }
        private Statement[] ElseStatements
        {
            get;
            set;
        }
        public IfStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            string conditionStr = match.Groups[1].Value;
            this.IfCondition = StatementTreeBuilder.Build(parent, conditionStr, lineIndex);
            int startIndex = Parser.FindNextOpenBracket(parent.ParentClass.File.Lines, lineIndex);
            int endIndex = Parser.GetBracketCloseIndex(parent.ParentClass.File.Brackets, startIndex);

            this.LineSize = (endIndex - startIndex) + 2;

            this.IfStatements = Parser.BuildStatementBlock(parent, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray();

            ParseElseStatement(endIndex);


        }
        private void ParseElseStatement(int ifEndIndex)
        {
            int nextIndex = Parser.FindNextInstructionIndex(Parent.ParentClass.File.Lines, ifEndIndex);

            if (nextIndex != -1)
            {
                string line = Parent.ParentClass.File.Lines[nextIndex].Trim();
                Match elseMatch = Regex.Match(line, ELSE_REGEX);

                if (elseMatch.Success)
                {
                    string elseConditionStr = elseMatch.Groups[3].Value;
                    this.ElseCondition = StatementTreeBuilder.Build(Parent, elseConditionStr, nextIndex);

                    int startIndex = Parser.FindNextOpenBracket(Parent.ParentClass.File.Lines, nextIndex);
                    int endIndex = Parser.GetBracketCloseIndex(Parent.ParentClass.File.Brackets, startIndex);

                    this.ElseStatements = Parser.BuildStatementBlock(Parent, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray();
                    LineSize += (endIndex - startIndex) + 2;
                }
            }

        }
        public IfStatement(IParentBlock parent) : base(parent)
        {

        }
        public override int GetLineSkip()
        {
            return LineSize;
        }


        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            int jumpIndex = context.ByteCodeLength + 1;

            IfCondition.GenerateBytecode(container,context);

            JumpIfFalseCode jumpIfFalse = new JumpIfFalseCode(-1);

            context.Results.Add(jumpIfFalse);

            foreach (var statement in IfStatements)
            {
                statement.GenerateBytecode(container,context);
            }

            JumpCode jumpElseIfTrue = new JumpCode(-1);
            context.Results.Add(jumpElseIfTrue);

            jumpIfFalse.targetIndex = context.ByteCodeLength + 1;

            JumpIfFalseCode jumpElseFalse = new JumpIfFalseCode(-1);

            if (ElseCondition != null) // else
            {
                if (ElseCondition.IsNull() == false) // else (...)
                {
                    ElseCondition.GenerateBytecode(container,context);

                    context.Results.Add(jumpElseFalse);

                }
                foreach (var statement in ElseStatements)
                {
                    statement.GenerateBytecode(container,context);
                }
            }

            jumpElseFalse.targetIndex = context.ByteCodeLength + 1;
            jumpElseIfTrue.targetIndex = context.ByteCodeLength + 1;


        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            
        }
    }
}
