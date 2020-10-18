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
        public override void EnterString([NotNull] StringContext context)
        {
            string value = context.STRING_LITERAL().GetText().Replace("\"", "");
            this.Result.Add(new ConstStringExpression(Result, context, value));
        }
        public override void EnterIntegerLiteral([NotNull] IntegerLiteralContext context)
        {
            this.Result.Add(new ConstIntExpression(Result, context, int.Parse(context.GetText())));
        }

        public override void EnterNativeCall([NotNull] NativeCallContext context)
        {
            NativeCallExpression expr = new NativeCallExpression(Result, context.IDENTIFIER().GetText(), context);
            expr.Parameters = GetMethodCallParameters(expr, context, context.expressionList());
            this.Result.Add(expr);
        }
        public override void EnterCtorCall([NotNull] CtorCallContext context)
        {
            StructCallCtorExpression expr = new StructCallCtorExpression(Result, context.constructorCall().creator().createdName().GetText(), context);
            expr.Parameters = GetMethodCallParameters(expr, context, context.constructorCall().creator().classCreatorRest().arguments().expressionList());
            this.Result.Add(expr);
        }

        public override void EnterMethodAccessor([NotNull] MethodAccessorContext context)
        {
            string name = context.expression().GetText() + "." + context.methodCall().IDENTIFIER().GetText();
            MethodCallExpression expr = new MethodCallExpression(Result, context, name);
            expr.Parameters = GetMethodCallParameters(expr, context, context.methodCall().expressionList());
            this.Result.Add(expr);
        }
        public override void EnterFieldAccessor([NotNull] FieldAccessorContext context)
        {
            string name = context.GetText();
            Result.Add(new VariableNameExpression(Result, context, name));
        }
        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context, context.IDENTIFIER().GetText());
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

                    ExpressionNode result = listener.GetResult();
                    results.Add(result);


                }
            }

            return results;
        }


        public override void EnterOpExpr([NotNull] OpExprContext context)
        {
            if (context.prefix != null)
            {
                throw new NotImplementedException("Unary operator not handled yet.");
            }
            else
            {
                this.Result.Add(new OperatorExpression(Result, context.bop.Text, context));
                context.right.EnterRule(this);
                context.left.EnterRule(this);
            }
        }
        public override void EnterNtvCall([NotNull] NtvCallContext context)
        {
            context.nativeCall().EnterRule(this);
        }
        public override void EnterMetCall([NotNull] MetCallContext context)
        {
            context.methodCall().EnterRule(this);
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

        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
