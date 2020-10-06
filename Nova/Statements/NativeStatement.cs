using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using Nova.Members;
using Nova.Semantics;
using Nova.Utils;

namespace Nova.Statements
{
    public class NativeStatement : Statement
    {
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
        private NativesEnum NativeEnum
        {
            get;
            set;
        }
        public NativeStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.NativeName = match.Groups[1].Value;
            string parametersStr = match.Groups[2].Value;
            this.Parameters = StatementTreeBuilder.BuildNodeCollection(parent, parametersStr, lineIndex, TokenType.Comma);
        }
        public NativeStatement(IParentBlock parent, string line, int lineIndex, string name, StatementNode[] parameters) : base(parent, line, lineIndex)
        {
            this.NativeName = name;
            this.Parameters = parameters;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.GenerateBytecode(container, context);
            }

            context.Instructions.Add(new NativeCallCode((int)NativeEnum));

        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            NativesEnum result = NativesEnum.Unknown;

            if (!Enum.TryParse(NativeName, out result) || result == NativesEnum.Unknown)
            {
                validator.AddError("Unknown native function : " + NativeName, LineIndex);
            }

            NativeEnum = result;
            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }

    }
}
