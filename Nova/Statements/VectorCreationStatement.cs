using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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


        private StatementNode[] Elements
        {
            get;
            set;
        }
        public VectorCreationStatement(IChild parent) : base(parent)
        {

        }

        public VectorCreationStatement(IChild parent, string parametersStr, int lineIndex) : base(parent, parametersStr, lineIndex)
        {
            //    this.Elements = StatementTreeBuilder.BuildNodeCollection(parent, parametersStr, lineIndex, TokenType.Comma);
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Class targetClass = container.TryGetClass(Constants.STD_VECTOR_CLASSNAME);

            context.Instructions.Add(new StructCreateCode(container.GetClassId(Constants.STD_VECTOR_CLASSNAME)));

            foreach (var element in Elements)
            {
                element.GenerateBytecode(container, context);
            }

            context.Instructions.Add(new VectCreateCode(Elements.Length));

            context.Instructions.Add(new CtorCallCode(targetClass.GetCtor().Id, 1));
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            foreach (var element in Elements)
            {
                element.ValidateSemantics(validator);
            }
        }
    }
}
