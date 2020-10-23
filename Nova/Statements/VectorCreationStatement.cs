using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Statements
{
    public class VectorCreationStatement : Statement
    {
        private List<ExpressionNode> Elements
        {
            get;
            set;
        }

        public VectorCreationStatement(IChild parent, List<ExpressionNode> elements, ParserRuleContext context) : base(parent, context)
        {
            this.Elements = elements;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Class targetClass = container.TryGetClass(Constants.STDVectorClassName);

            context.Instructions.Add(new StructCreateCode(container.GetClassId(Constants.STDVectorClassName)));

            foreach (var element in Elements)
            {
                element.GenerateBytecode(container, context);
            }

            context.Instructions.Add(new VectCreateCode(Elements.Count));

            context.Instructions.Add(new CtorCallCode(targetClass.GetCtor().Id, 1));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            foreach (var element in Elements)
            {
                element.ValidateSemantics(validator);
            }
        }

        public override void ValidateTypes(SemanticsValidator validator)
        {
                
        }
    }
}
