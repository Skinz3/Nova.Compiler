using Antlr4.Runtime;
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
            else
            {
                foreach (var rule in context.literal().GetRuleContexts<ParserRuleContext>())
                {
                    rule.EnterRule(this);
                }
            }
        }
        public override void EnterPrimaryValue([NotNull] PrimaryValueContext context)
        {
            context.primary().EnterRule(this);
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
            expr.Parameters = GetMethodParameters(expr, context, context.expressionList());

            this.Result.Add(expr);
        }

        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context, context.IDENTIFIER().GetText());

            List<ExpressionNode> parameters = new List<ExpressionNode>();

            expr.Parameters = GetMethodParameters(expr, context, context.expressionList());

            this.Result.Add(expr);
        }
        private List<ExpressionNode> GetMethodParameters(IChild parent, ParserRuleContext context, ExpressionListContext expressionListContext)
        {
            List<ExpressionNode> parameters = new List<ExpressionNode>();

            if (expressionListContext != null)
            {
                foreach (var expression in expressionListContext.GetRuleContexts<ParserRuleContext>())
                {
                    ExpressionListener listener = new ExpressionListener(parent);

                    expression.EnterRule(listener);

                    ExpressionNode result = listener.GetResult();

                    parameters.Add(result);

                }
            }

            return parameters;
        }
        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
