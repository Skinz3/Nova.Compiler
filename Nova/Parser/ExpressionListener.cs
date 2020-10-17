using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.Expressions;
using Nova.Lexer;
using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NovaParser;

namespace Nova.Parser
{
    public class ExpressionListener : NovaParserBaseListener
    {
        private IChild Parent
        {
            get;
            set;
        }
        private ExpressionNode Result
        {
            get;
            set;
        }
        public ExpressionListener(IChild parent)
        {
            this.Parent = parent;
            this.Result = new ExpressionNode(Parent);
        }

        public override void EnterIntegerLiteral([NotNull] IntegerLiteralContext context)
        {
            this.Result.Add(new ConstIntExpression(Result, context, int.Parse(context.GetText())));
        }
        public override void EnterOpExpr([NotNull] OpExprContext context)
        {
            this.Result.Add(new OperatorExpression(Result, context.bop.Text, context));
        }
        public override void EnterNativeCall([NotNull] NativeCallContext context)
        {
            this.Result.Add(new Nativeca)
            base.EnterNativeCall(context);
        }
        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
