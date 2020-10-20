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
using Antlr4.Runtime;

namespace Nova.Statements
{
    public class IfStatement : Statement
    {
        public ExpressionNode IfCondition
        {
            get;
            set;
        }
        public List<Statement> IfStatements
        {
            get;
            set;
        }
        public List<Statement> ElseStatements
        {
            get;
            set;
        }

        public IfStatement(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int jumpIndex = context.NextOpIndex;

            IfCondition.GenerateBytecode(container, context);

            JumpIfFalseCode jumpIfFalse = new JumpIfFalseCode(-1);

            context.Instructions.Add(jumpIfFalse);

            foreach (var statement in IfStatements)
            {
                statement.GenerateBytecode(container, context);
            }

            JumpCode jumpElseIfTrue = new JumpCode(-1);
            context.Instructions.Add(jumpElseIfTrue);

            jumpIfFalse.targetIndex = context.NextOpIndex;

            if (ElseStatements != null)
            {
                foreach (var statement in ElseStatements)
                {
                    statement.GenerateBytecode(container, context);
                }
            }

            jumpElseIfTrue.targetIndex = context.NextOpIndex;


        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            IfCondition.ValidateSemantics(validator);

            foreach (var statement in IfStatements)
            {
                statement.ValidateSemantics(validator);
            }

            if (ElseStatements != null)
            {
                foreach (var st in ElseStatements)
                {
                    st.ValidateSemantics(validator);
                }
            }
        }
    }
}

