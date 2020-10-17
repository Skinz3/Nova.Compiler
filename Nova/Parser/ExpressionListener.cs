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
        public override void EnterPrimary([NotNull] PrimaryContext context)
        {
            var identifier = context.IDENTIFIER();

            if (identifier != null)
            {
                Result.Add(new VariableNameExpression(Result, context, identifier.GetText()));
            }
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
            NativeCallExpression expr = new NativeCallExpression(Result, context.IDENTIFIER().GetText(), context);

            List<ExpressionNode> parameters = new List<ExpressionNode>();

            ExpressionListContext expressionListContext = context.expressionList();

            if (expressionListContext != null)
            {
                foreach (var expression in expressionListContext.expression())
                {
                    ExpressionListener listener = new ExpressionListener(expr); // same here

                    ParseTreeWalker.Default.Walk(listener, expression);

                    parameters.Add(listener.GetResult());
                }
            }

            expr.Parameters = parameters;

            this.Result.Add(expr);
        }
        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context, context.IDENTIFIER().GetText());

            List<ExpressionNode> parameters = new List<ExpressionNode>();

            ExpressionListContext expressionListContext = context.expressionList();

            if (expressionListContext != null)
            {
                foreach (var expression in expressionListContext.expression())
                {
                    ExpressionListener listener = new ExpressionListener(expr); // same here

                    ParseTreeWalker.Default.Walk(listener, expression);

                    parameters.Add(listener.GetResult());
                }
            }

            expr.Parameters = parameters;

            this.Result.Add(expr);
        }
        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
