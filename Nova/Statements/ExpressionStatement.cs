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
using System.Net;
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

            foreach (var expr in tree)
            {
                if (expr is not MethodCallExpression && expr is not NativeCallExpression && expr is not VariableNameExpression)
                {
                    validator.AddError("Forbidenn expression statement (" + expr.GetType().Name + ")", base.ParsingContext);
                    return;
                }
            }


            Expression.ValidateSemantics(validator);
        }
    }
}
