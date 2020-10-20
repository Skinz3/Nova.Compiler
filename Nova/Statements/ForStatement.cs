using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class ForStatement : Statement
    {
        private Statement DeclarationStatement
        {
            get;
            set;
        }
        private ExpressionNode BeginNode
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
      
        public ForStatement(IChild parent, Statement declarationStatement, ExpressionNode beginNode, Statement assignationStatement,
            List<Statement> statements, ParserRuleContext context) : base(parent, context)
        {
            /* this.DeclarationStatement = StatementBuilder.Build(parent, match.Groups[1].Value, lineIndex);
             this.BeginNode = StatementTreeBuilder.Build(parent, match.Groups[2].Value, lineIndex);
             this.AssignationStatement = StatementBuilder.Build(parent, match.Groups[3].Value, lineIndex);

             int startIndex = Parser.FindNextOpenBracket(parent.ParentClass.File.Lines, lineIndex);
             int endIndex = Parser.GetBracketCloseIndex(parent.ParentClass.File.Brackets, startIndex);

             this.LineSize = (endIndex - startIndex) + 2;

             this.Statements = Parser.BuildStatementBlock(this, startIndex + 1, endIndex, Parent.ParentClass.File.Lines).ToArray(); */
        }

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

    }
}
