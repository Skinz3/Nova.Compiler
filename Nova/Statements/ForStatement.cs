using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class ForStatement : Statement, IChild, IStatementBlock
    {
        public const string REGEX = @"^for \((.+);(.+);(.+)\)$"; // for i from 1 to 3

        private Statement DeclarationStatement
        {
            get;
            set;
        }
        private StatementNode BeginNode
        {
            get;
            set;
        }
        private Statement AssignationStatement
        {
            get;
            set;
        }
        public List<Statement> Statements
        {
            get;
            private set;
        }
        private int LineSize
        {
            get;
            set;
        }
        public ForStatement(IChild parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            /* this.DeclarationStatement = StatementBuilder.Build(parent, match.Groups[1].Value, lineIndex);
             this.BeginNode = StatementTreeBuilder.Build(parent, match.Groups[2].Value, lineIndex);
             this.AssignationStatement = StatementBuilder.Build(parent, match.Groups[3].Value, lineIndex);

             int startIndex = Parser.FindNextOpenBracket(parent.ParentClass.File.Lines, lineIndex);
             int endIndex = Parser.GetBracketCloseIndex(parent.ParentClass.File.Brackets, startIndex);

             this.LineSize = (endIndex - startIndex) + 2;

             this.Statements = Parser.BuildStatementBlock(this, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray(); */
        }

        public Class ParentClass => this.Parent.ParentClass;

        IChild IChild.Parent => this.Parent;

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            DeclarationStatement.GenerateBytecode(container, context);

            int jumpIndex = context.NextOpIndex;

            BeginNode.GenerateBytecode(container, context);

            JumpIfFalseCode code = new JumpIfFalseCode(-1);
            context.Instructions.Add(code);

            foreach (var statement in this.Statements)
            {
                statement.GenerateBytecode(container, context);
            }

            AssignationStatement.GenerateBytecode(container, context);

            context.Instructions.Add(new JumpCode(jumpIndex));
            code.targetIndex = context.NextOpIndex;




        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            validator.BlockStart();

            DeclarationStatement.ValidateSemantics(validator);
            BeginNode.ValidateSemantics(validator);
            AssignationStatement.ValidateSemantics(validator);

            foreach (var statement in Statements)
            {
                statement.ValidateSemantics(validator);
            }

            validator.BlockEnd();
        }
        public override int GetLineSkip()
        {
            return LineSize;
        }

    }
}
