using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.ByteCode.Codes;
using Nova.Expressions;
using Nova.Lexer;
using Nova.Members;
using Nova.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NovaParser;

namespace Nova.Parser.Listeners
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
            foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
            {
                rule.EnterRule(this);
            }
        }
        public override void EnterPrimaryExpr([NotNull] PrimaryExprContext context)
        {
            foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
            {
                rule.EnterRule(this);
            }
        }

        public override void EnterPrimarylit([NotNull] PrimarylitContext context)
        {
            foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
            {
                rule.EnterRule(this);
            }

        }


        public override void EnterChar([NotNull] CharContext context)
        {
            base.EnterChar(context);
        }
        public override void EnterBool([NotNull] BoolContext context)
        {
            bool value = bool.Parse(context.BOOL_LITERAL().GetText());
            this.Result.Insert(new ConstBoolExpression(Result, context, value));
        }
        public override void EnterNull([NotNull] NullContext context)
        {
            base.EnterNull(context);
        }
        public override void EnterString([NotNull] StringContext context)
        {
            string value = context.STRING_LITERAL().GetText().Replace("\"", "");
            this.Result.Insert(new ConstStringExpression(Result, context, value));
        }
        public override void EnterInt([NotNull] IntContext context)
        {
            this.Result.Insert(new ConstIntExpression(Result, context, int.Parse(context.GetText())));
        }

        public override void EnterNativeCall([NotNull] NativeCallContext context)
        {
            NativeCallExpression expr = new NativeCallExpression(Result, context.IDENTIFIER().GetText(), context);
            expr.Parameters = GetMethodCallParameters(expr, context, context.expressionList());
            this.Result.Insert(expr);
        }
        public override void EnterCtorCall([NotNull] CtorCallContext context)
        {
            StructCallCtorExpression expr = new StructCallCtorExpression(Result, context.constructorCall().creator().createdName().GetText(), context);
            expr.Parameters = GetMethodCallParameters(expr, context, context.constructorCall().creator().classCreatorRest().arguments().expressionList());
            this.Result.Insert(expr);
        }

        public override void EnterMethodAccessor([NotNull] MethodAccessorContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context);

            expr.Name = context.methodCall().IDENTIFIER().GetText();

            ExpressionListener expressionListener = new ExpressionListener(expr);
            context.expression().EnterRule(expressionListener);
            expr.Accessor = expressionListener.Result;

            expr.Parameters = GetMethodCallParameters(expr, context, context.methodCall().expressionList());

            this.Result.Insert(expr);
        }
        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            MethodCallExpression expr = new MethodCallExpression(Result, context);

            expr.Name = context.IDENTIFIER().GetText();
            expr.Accessor = new ExpressionNode(expr);
            expr.Parameters = GetMethodCallParameters(expr, context, context.expressionList());
            this.Result.Insert(expr);
        }
        public override void EnterFieldAccessor([NotNull] FieldAccessorContext context)
        {
            VariableNameExpression expr = new VariableNameExpression(Result, context);

            expr.Name = context.IDENTIFIER().GetText();

            ExpressionListener expressionListener = new ExpressionListener(expr);
            context.expression().EnterRule(expressionListener);
            expr.AccessorExpression = expressionListener.Result.Get(0);

            this.Result.Insert(expr);

        }
        public override void EnterLitIdent([NotNull] LitIdentContext context)
        {
            VariableNameExpression expression = new VariableNameExpression(Result, context);
            expression.Name = context.GetText();
            expression.AccessorExpression = null;
            Result.Insert(expression);
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

        public override void EnterUnaryExpr([NotNull] UnaryExprContext context)
        {
            Logger.Write("Unary operators are not handled properly.", LogType.Warning);

            this.Result.Insert(new OperatorExpression(Result, context.prefix.Text, context));
            context.expression().EnterRule(this);
            this.Result.Insert(new ConstIntExpression(Parent, context, 0));

        }
        public override void EnterBinaryExpr([NotNull] BinaryExprContext context)
        {
            this.Result.Insert(new OperatorExpression(Result, context.bop.Text, context));
            context.right.EnterRule(this);
            context.left.EnterRule(this);
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
                rule.EnterRule(this);  // integer litteral  , bool litteral etc
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
