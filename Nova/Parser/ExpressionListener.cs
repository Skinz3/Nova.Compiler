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

                foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
                {
                    rule.EnterRule(this);
                }
            }
        }
        public override void EnterLiteral([NotNull] LiteralContext context)
        {
            foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
            {
                rule.EnterRule(this);  // integer litteral  , float litteral etc
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

            context.right.EnterRule(this);
            context.left.EnterRule(this);

        }

        public override void EnterNativeCall([NotNull] NativeCallContext context)
        {
            NativeCallExpression expr = new NativeCallExpression(Result, context.IDENTIFIER().GetText(), context);

            List<ExpressionNode> parameters = new List<ExpressionNode>();
            expr.Parameters = GetMethodCallParameters(expr, context, context.expressionList());

            this.Result.Add(expr);
        }

        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context, context.IDENTIFIER().GetText());

            List<ExpressionNode> parameters = new List<ExpressionNode>();

            expr.Parameters = GetMethodCallParameters(expr, context, context.expressionList());

            this.Result.Add(expr);
        }
        private List<ExpressionNode> GetMethodCallParameters(IChild parent, ParserRuleContext context, ExpressionListContext expressionListContext)
        {
            List<ExpressionNode> results = new List<ExpressionNode>();

            if (expressionListContext != null)
            {
                var parameters = expressionListContext.GetRuleContexts<ParserRuleContext>();

                foreach (var parameter in parameters)
                {
                    ExpressionListener listener = new ExpressionListener(parent);

                    parameter.EnterRule(listener);

                    foreach (var expression in parameter.GetRuleContexts<ParserRuleContext>())
                    {
                        expression.EnterRule(listener);
                    }

                    ExpressionNode result = listener.GetResult();
                    results.Add(result);


                }
            }

            return results;
        }
        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
