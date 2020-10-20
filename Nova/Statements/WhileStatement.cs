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
        public ExpressionNode Condition
        {
            get;
            set;
        }
        public List<Statement> Statements
        {
            get;
            set;
        }

        public WhileStatement(IChild parent, ParserRuleContext context) : base(parent, context)
        {



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
