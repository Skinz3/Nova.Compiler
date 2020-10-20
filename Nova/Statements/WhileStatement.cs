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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class WhileStatement : Statement
    {
        public const string REGEX = @"^while\s*\((.+)\)\s*({)?$";

        private ExpressionNode Condition
        {
            get;
            set;
        }
        public List<Statement> Statements
        {
            get;
            private set;
        }

        public WhileStatement(IChild parent, ExpressionNode condition, List<Statement> statements, ParserRuleContext context) : base(parent, context)
        {
            /*  string conditionStr = match.Groups[1].Value;

              this.Condition = StatementTreeBuilder.Build(parent, conditionStr, lineIndex);

              int startIndex = Parser.FindNextOpenBracket(parent.ParentClass.File.Lines, lineIndex);
              int endIndex = Parser.GetBracketCloseIndex(parent.ParentClass.File.Brackets, startIndex);

              this.LineSize = (endIndex - startIndex) + 2;

              this.Statements = Parser.BuildStatementBlock(this, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray(); */


        }
   

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int jumpIndex = context.NextOpIndex;

            Condition.GenerateBytecode(container, context);

            JumpIfFalseCode jumpIfFalse = new JumpIfFalseCode(-1);

            context.Instructions.Add(jumpIfFalse);

            foreach (var statement in Statements)
            {
                statement.GenerateBytecode(container, context);
            }

            context.Instructions.Add(new JumpCode(jumpIndex));

            jumpIfFalse.targetIndex = context.NextOpIndex;

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
