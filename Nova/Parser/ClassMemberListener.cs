using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.ByteCode.Enums;
using Nova.Lexer;
using Nova.Members;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NovaParser;

namespace Nova.Parser
{
    public class ClassMemberListener : NovaParserBaseListener
    {
        private Class Class
        {
            get;
            set;
        }

        public ClassMemberListener(Class @class)
        {
            this.Class = @class;
        }
        public override void EnterMemberDeclaration([NotNull] NovaParser.MemberDeclarationContext context)
        {
            foreach (var memberDeclaration in context.GetRuleContexts<ParserRuleContext>())
            {
                memberDeclaration.EnterRule(this);
            }
        }
        public override void EnterMethodDeclaration([NotNull] NovaParser.MethodDeclarationContext context)
        {
            NovaParser.MemberDeclarationContext parent = (NovaParser.MemberDeclarationContext)context.parent;

            string returnType = context.typeTypeOrUnit().GetText();
            string methodName = context.IDENTIFIER().GetText();
            ModifiersEnum modifiers = ParserUtils.ParseModifier(parent.modifier().classModifier().GetText());

            AddMethod(methodName, returnType, modifiers, context, context.formalParameters());
        }
        public override void EnterFieldDeclaration([NotNull] NovaParser.FieldDeclarationContext context)
        {
            NovaParser.MemberDeclarationContext parent = (NovaParser.MemberDeclarationContext)context.parent;
            ModifiersEnum modifiers = ParserUtils.ParseModifier(parent.modifier().classModifier().GetText());

            VariableDeclaratorContext declarator = context.variableDeclarator();

            string type = context.typeType().GetText();
            string name = declarator.variableDeclaratorId().GetText();

            Field field = new Field(Class, Class.PopFieldId(), modifiers, new Variable(name, type));

            ExpressionNode value = new ExpressionNode(field);

            var initializer = declarator.variableInitializer();

            if (initializer != null)
            {
                ExpressionContext expressionContext = initializer.expression();

                ExpressionListener listener = new ExpressionListener(field); // same here

                foreach (var expression in expressionContext.GetRuleContexts<ParserRuleContext>())
                {
                    expression.EnterRule(listener);
                }

                value = listener.GetResult();
            }

            field.Value = value;

            Class.Fields.Add(field.Name, field);

        }
        public override void EnterConstructorDeclaration([NotNull] NovaParser.ConstructorDeclarationContext context)
        {
            NovaParser.MemberDeclarationContext parent = (NovaParser.MemberDeclarationContext)context.parent;

            string returnType = string.Empty; // must be unit
            string methodName = context.IDENTIFIER().GetText();
            ModifiersEnum modifiers = ModifiersEnum.ctor; // this is not a modifier ! 

            AddMethod(methodName, returnType, modifiers, context, context.formalParameters());
        }
        private void AddMethod(string methodName, string returnType, ModifiersEnum modifiers, ParserRuleContext context, FormalParametersContext parameterContext)
        {
            List<Variable> parameters = new List<Variable>();

            FormalParameterListContext parameterListContext = parameterContext.formalParameterList();

            if (parameterListContext != null)
            {
                foreach (var parameter in parameterListContext.formalParameter())
                {
                    parameters.Add(new Variable(parameter.variableDeclaratorId().GetText(), parameter.typeType().GetText()));
                }
            }


            Method method = new Method(Class, Class.PopMethodId(), methodName, modifiers, returnType,
               parameters,
                context.start.Line,
                context.stop.Line,
                new List<Statement>(), context);


            StatementListener listener = new StatementListener(method);

            foreach (var methodRule in context.GetRuleContexts<ParserRuleContext>())
            {
                methodRule.EnterRule(listener);
            }

            Class.Methods.Add(method.Name, method);
        }
    }
}
