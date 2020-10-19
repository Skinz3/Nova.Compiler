using Antlr4.Runtime;
using Nova.ByteCode.Generation;
using Nova.Expressions;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class ExpressionStatement : Statement
    {
        public ExpressionNode Expression
        {
            get;
            set;
        }
        public ExpressionStatement(IChild parent, ParserRuleContext ruleContext) : base(parent, ruleContext)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Expression.GenerateBytecode(container, context);
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            var tree = Expression.GetTree();

            if (tree.Count() > 1)
            {
                validator.AddError("Invalid expression", base.ParsingContext);
            }

            var expr = tree.ElementAt(0);

            if (!(expr is MethodCallExpression) || !(expr is NativeCallExpression))
            {
                validator.AddError("Forbidenn expression statement (" + expr.GetType().Name + ")", base.ParsingContext);
            }



            Expression.ValidateSemantics(validator);
        }
    }
}
