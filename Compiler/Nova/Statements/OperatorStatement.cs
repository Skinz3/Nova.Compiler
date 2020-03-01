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
    public class OperatorStatement : Statement
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

        public OperatorStatement(IParentBlock parent, string @operator, int lineIndex) : base(parent, @operator.ToString(), lineIndex)
        {
            this.Operator = @operator;
        }
        public OperatorStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            switch (OperatorEnum)
            {
                case OperatorsEnum.Plus:
                    context.Results.Add(new ArithmeticCode(OperatorsEnum.Plus));
                    break;
                case OperatorsEnum.Minus:
                    context.Results.Add(new ArithmeticCode(OperatorsEnum.Minus));
                    break;
                case OperatorsEnum.Multiply:
                    context.Results.Add(new ArithmeticCode(OperatorsEnum.Multiply));
                    break;
                case OperatorsEnum.Inferior:
                    context.Results.Add(new ComparaisonCode(OperatorsEnum.Inferior));
                    break;
                case OperatorsEnum.Superior:
                    context.Results.Add(new ComparaisonCode(OperatorsEnum.Superior));
                    break;
                case OperatorsEnum.Different:
                    context.Results.Add(new ComparaisonCode(OperatorsEnum.Different));
                    break;
                case OperatorsEnum.Equals:
                    context.Results.Add(new ComparaisonCode(OperatorsEnum.Equals));
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
