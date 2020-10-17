using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using Nova.Utils;

namespace Nova.Statements
{
    public class NativeStatement : Statement
    {
        private string NativeName
        {
            get;
            set;
        }
        private List<Expression> Parameters
        {
            get;
            set;
        }
        private NativesEnum NativeEnum
        {
            get;
            set;
        }
        public NativeStatement(IChild parent, string nativeName, List<Expression> parameters, NativesEnum native, ParserRuleContext context) : base(parent, context)
        {
            this.NativeName = nativeName;
            this.Parameters = parameters;
            this.NativeEnum = native;
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
