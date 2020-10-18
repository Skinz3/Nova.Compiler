using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.Lexer;
using Nova.Members;
using Nova.Parser.Accessors;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NovaParser;

namespace Nova.Parser
{
    public class StatementListener : NovaParserBaseListener
    {
        private IStatementBlock Block
        {
            get;
            set;
        }

        public StatementListener(IStatementBlock block)
        {
            this.Block = block;
        }
        public override void EnterMethodBody([NotNull] MethodBodyContext context)
        {
            foreach (var blockContext in context.GetRuleContexts<BlockContext>())
            {
                foreach (var rule in blockContext.GetRuleContexts<ParserRuleContext>())
                {
                    rule.EnterRule(this);
                }

            }
        }
        public override void EnterStatement([NotNull] StatementContext context)
        {
            foreach (var statement in context.GetRuleContexts<ParserRuleContext>())
            {
                statement.EnterRule(this);
            }
        }
        public override void EnterLocalVariableDeclaration([NotNull] NovaParser.LocalVariableDeclarationContext context)
        {
            VariableDeclaratorContext declarator = context.variableDeclarator();

            string type = context.typeType().GetChild(0).GetText();
            string name = declarator.variableDeclaratorId().GetText();

            DeclarationStatement statement = new DeclarationStatement(Block, context);

            Variable variable = new Variable(name, type);

            ExpressionNode value = new ExpressionNode(statement);

            VariableInitializerContext initializer = declarator.variableInitializer();

            if (initializer != null)
            {
                ExpressionContext expressionContext = initializer.expression();

                ExpressionListener listener = new ExpressionListener(statement);

                foreach (var child in expressionContext.GetRuleContexts<ParserRuleContext>())
                {
                    child.EnterRule(listener);
                }

                value = listener.GetResult();

            }

            statement.Variable = variable;
            statement.Value = value;

            Block.Statements.Add(statement);
        }
        public override void EnterStatementExpression([NotNull] StatementExpressionContext context)
        {
            ExpressionStatement statement = new ExpressionStatement(Block, context);

            ExpressionListener listener = new ExpressionListener(statement);

            foreach (var child in context.expression().GetRuleContexts<ParserRuleContext>())
            {
                child.EnterRule(listener);
            }

            ExpressionNode value = listener.GetResult();

            statement.Expression = value;

            Block.Statements.Add(statement);
        }

    }

}
