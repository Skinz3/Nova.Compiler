using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.Bytecode.Enums;
using Nova.IO;
using Nova.Members;
using System;
using static NovaParser;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace Nova.Parser.Listeners
{
    public partial class ClassListener : NovaParserBaseListener
    {
        private NvFile File
        {
            get;
            set;
        }
        public ClassListener(NvFile file)
        {
            this.File = file;
        }

        public override void EnterTypeDeclaration([NotNull] TypeDeclarationContext context)
        {
            foreach (var rule in context.GetRuleContexts<ParserRuleContext>())
            {
                rule.EnterRule(this);
            }
        }
        public override void EnterPrimitiveDeclaration([NotNull] PrimitiveDeclarationContext context)
        {
            string name = context.primitiveTypeOrUnit().GetText();
            AddMember(name, ContainerType.primitive, context);
        }
        public override void EnterClassDeclaration(NovaParser.ClassDeclarationContext context)
        {
            string name = context.IDENTIFIER().GetText();
            AddMember(name, ContainerType.@class, context);
        }
        public override void EnterStructDeclaration([NotNull] NovaParser.StructDeclarationContext context)
        {
            AddMember(context.IDENTIFIER().GetText(), ContainerType.@struct, context);
        }

        private void AddMember(string className, ContainerType type, ParserRuleContext context)
        {
            Class @class = new Class(File, className, type, context);
            ClassMemberListener listener = new ClassMemberListener(@class);

            foreach (var memberDeclaration in context.GetRuleContext<ClassBodyContext>(0).GetRuleContexts<ParserRuleContext>())
            {
                memberDeclaration.EnterRule(listener);
            }

            File.Classes.Add(@class);
        }


    }
}