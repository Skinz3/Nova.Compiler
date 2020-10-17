using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.ByteCode.Enums;
using Nova.Members;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
              
        }
        public override void EnterMethodDeclaration([NotNull] NovaParser.MethodDeclarationContext context)
        {
            NovaParser.MemberDeclarationContext parent = (NovaParser.MemberDeclarationContext)context.parent;

            string returnType = context.typeTypeOrUnit().GetText();
            string methodName = context.IDENTIFIER().GetText();
            ModifiersEnum modifiers = ParserUtils.ParseModifier(parent.modifier().classModifier().GetText());

            Method method = new Method(Class, -1, methodName, modifiers, returnType,
                new List<Variable>(),
                context.start.Line,
                context.stop.Line,
                new List<Statement>());

            StatementBlockListener listener = new StatementBlockListener(method);

            ParseTreeWalker.Default.Walk(listener, context);

            Class.Methods.Add(method.Name, method);
        }
        public override void EnterFieldDeclaration([NotNull] NovaParser.FieldDeclarationContext context)
        {

        }
        public override void EnterConstructorDeclaration([NotNull] NovaParser.ConstructorDeclarationContext context)
        {

        }
    }
}
