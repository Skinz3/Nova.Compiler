using Antlr4.Runtime;
using Nova.Bytecode.Enums;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions
{
    public class NativeCallExpression : Expression
    {
        private string NativeName
        {
            get;
            set;
        }
        public List<ExpressionNode> Parameters
        {
            get;
            set;
        }
        private NativesEnum NativeEnum
        {
            get;
            set;
        }
        public NativeCallExpression(IChild parent, string nativeName, ParserRuleContext context) : base(parent, context)
        {
            this.NativeName = nativeName;
            this.NativeName = nativeName;
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
                validator.AddError("Unknown native function : " + NativeName, ParsingContext);
            }

            NativeEnum = result;
            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }
        }

    }
}
