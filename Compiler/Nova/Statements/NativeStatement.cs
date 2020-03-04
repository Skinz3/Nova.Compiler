using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class NativeStatement : Statement
    {
        static Dictionary<string, Type> Natives = new Dictionary<string, Type>()
        {
            { "Printl", typeof(PrintlCode) },
            { "Readl",typeof(ReadlCode) }

        };



        public static string REGEX = @"^~([a-zA-Z_$][a-zA-Z_$0-9]*)\((.*)\)$";

        private string NativeName
        {
            get;
            set;
        }
        private StatementNode[] Parameters
        {
            get;
            set;
        }
        public NativeStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.NativeName = match.Groups[1].Value;
            string parametersStr = match.Groups[2].Value;
            this.Parameters = Parser.ParseMethodCallParameters(parent, lineIndex, parametersStr);
        }
        public NativeStatement(IParentBlock parent, string line, int lineIndex, string name, StatementNode[] parameters) : base(parent, line, lineIndex)
        {
            this.NativeName = name;
            this.Parameters = parameters;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.GenerateBytecode(container,context);
            }

            Type nativeType = Natives[NativeName];
            ICode code = (ICode)Activator.CreateInstance(nativeType);
            context.Results.Add(code);

        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!Natives.ContainsKey(NativeName))
            {
                validator.AddError("Unknown native function :" + NativeName, LineIndex);
            }
            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }

    }
}
