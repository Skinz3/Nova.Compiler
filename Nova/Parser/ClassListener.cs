using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Nova.Bytecode.Enums;
using Nova.IO;
using Nova.Members;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace Nova.Parser
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

        public override void EnterClassDeclaration(NovaParser.ClassDeclarationContext context)
        {
            AddMember(context.IDENTIFIER().GetText(), ContainerType.@class, context.start.Line, context.stop.Line, context);
        }
        public override void EnterStructDeclaration([NotNull] NovaParser.StructDeclarationContext context)
        {
            AddMember(context.IDENTIFIER().GetText(), ContainerType.@struct, context.start.Line, context.stop.Line, context);
        }

        private void AddMember(string className, ContainerType type, int startLine, int endLine, IParseTree context)
        {
            Class @class = new Class(File, className, type, startLine, endLine);
            ClassMemberListener listener = new ClassMemberListener(@class);
            ParseTreeWalker.Default.Walk(listener, context);
            File.Classes.Add(@class);
        }


    }
}