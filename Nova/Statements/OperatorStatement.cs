using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Enums;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class OperatorStatement : Statement // shouldnt be a statement....
    {
        private static readonly Dictionary<string, OperatorsEnum> OPERATORS_BINDING = new Dictionary<string, OperatorsEnum>()
        {
            { "+",OperatorsEnum.Plus },
            { "-",OperatorsEnum.Minus },
            { "*",OperatorsEnum.Multiply },
            { "/",OperatorsEnum.Divide },
            { "==",OperatorsEnum.Equals },
            { "!=",OperatorsEnum.Different },
            { "<", OperatorsEnum.Inferior },
            { ">",OperatorsEnum.Superior },
        };

        public string Operator
        {
            get;
            private set;
        }
        public OperatorsEnum OperatorEnum
        {
            get
            {
                return OPERATORS_BINDING[Operator];
            }
        }

        public OperatorStatement(IChild parent, string @operator, ParserRuleContext context) : base(parent, context)
        {
            this.Operator = @operator;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {

            switch (OperatorEnum)
            {
                case OperatorsEnum.Plus:
                    context.Instructions.Add(new AddCode());
                    break;
                case OperatorsEnum.Minus:
                    context.Instructions.Add(new SubCode());
                    break;
                case OperatorsEnum.Multiply:
                    context.Instructions.Add(new MulCode());
                    break;
                case OperatorsEnum.Divide:
                    context.Instructions.Add(new DivCode());
                    break;
                case OperatorsEnum.Inferior:
                    context.Instructions.Add(new ComparaisonCode(OperatorsEnum.Inferior));
                    break;
                case OperatorsEnum.Superior:
                    context.Instructions.Add(new ComparaisonCode(OperatorsEnum.Superior));
                    break;
                case OperatorsEnum.Different:
                    context.Instructions.Add(new ComparaisonCode(OperatorsEnum.Different));
                    break;
                case OperatorsEnum.Equals:
                    context.Instructions.Add(new ComparaisonCode(OperatorsEnum.Equals));
                    break;
                default:
                    throw new Exception();
            }
        }
        public override string ToString()
        {
            return Operator;
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {

        }

    }
}
