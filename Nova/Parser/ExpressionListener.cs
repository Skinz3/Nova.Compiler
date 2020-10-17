using Antlr4.Runtime.Misc;
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
        public override void EnterExpression([NotNull] ExpressionContext context)
        {
            var child = context.GetChild(0);
            Console.WriteLine(child.GetText());
            base.EnterExpression(context);
        }
        public ExpressionNode GetResult()
        {
            return this.Result;
        }
    }
}
