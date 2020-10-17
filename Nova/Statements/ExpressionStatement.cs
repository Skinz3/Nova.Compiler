using Antlr4.Runtime;
using Nova.ByteCode.Generation;
using Nova.Expressions;
using Nova.IO;
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
        private Expression Expression
        {
            get;
            set;
        }
        public ExpressionStatement(IChild parent, Expression expression, ParserRuleContext ruleContext) : base(parent, ruleContext)
        {
            this.Expression = expression;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Expression.GenerateBytecode(container, context);
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Expression.ValidateSemantics(validator);
        }
    }
}
