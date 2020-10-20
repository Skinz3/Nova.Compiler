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
        public Statement Init
        {
            get;
            set;
        }
        public ExpressionNode Condition
        {
            get;
            set;
        }
        public Statement Update
        {
            get;
            set;
        }
        public List<Statement> Statements
        {
            get;
            set;
        }

        public ForStatement(IChild parent, ParserRuleContext context) : base(parent, context)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Init.GenerateBytecode(container, context);

            int jumpIndex = context.NextOpIndex;

            Condition.GenerateBytecode(container, context);

            JumpIfFalseCode code = new JumpIfFalseCode(-1);
            context.Instructions.Add(code);

            foreach (var statement in this.Statements)
            {
                statement.GenerateBytecode(container, context);
            }

            Update.GenerateBytecode(container, context);

            context.Instructions.Add(new JumpCode(jumpIndex));
            code.targetIndex = context.NextOpIndex;




        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            validator.BlockStart();

            Init.ValidateSemantics(validator);
            Condition.ValidateSemantics(validator);
            Update.ValidateSemantics(validator);

            foreach (var statement in Statements)
            {
                statement.ValidateSemantics(validator);
            }

            validator.BlockEnd();
        }

    }
}
