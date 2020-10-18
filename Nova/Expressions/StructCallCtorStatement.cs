using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;

namespace Nova.Expressions
{
    public class StructCallCtorExpression : Expression
    {
        private string CtorName
        {
            get;
            set;
        }
        public List<ExpressionNode> Parameters
        {
            get;
            set;
        }
        private Class StructClass
        {
            get;
            set;
        }
        private Method StructCtor
        {
            get;
            set;
        }

        public StructCallCtorExpression(IChild parent, string name, ParserRuleContext context) : base(parent, context)
        {
            this.CtorName = name;
        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {

            Class targetClass = container[CtorName];

            context.Instructions.Add(new StructCreateCode(container.GetClassId(CtorName)));

            if (StructCtor != null)
            {
                foreach (var parameter in Parameters)
                {
                    parameter.GenerateBytecode(container, context);
                }

                context.Instructions.Add(new CtorCallCode(targetClass.GetCtor().Id, Parameters.Count));
            }
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            foreach (var parameter in Parameters)
            {
                parameter.ValidateSemantics(validator);
            }

            this.StructClass = validator.Container.TryGetClass(CtorName);

            if (StructClass == null)
            {
                validator.AddError("Unknown struct type " + CtorName, ParsingContext);
            }
            if (StructClass.Type != ContainerType.@struct)
            {
                validator.AddError("Constructors can only be called on struct", ParsingContext);
            }
           
            this.StructClass = validator.Container[CtorName];

            this.StructCtor = StructClass.GetCtor();

            if (StructCtor == null)
            {
                validator.AddError("Unknown struct ctor \"" + StructClass.ClassName + "\"", ParsingContext);
            }
            else if (Parameters.Count != StructCtor.Parameters.Count)
            {
                validator.AddError("Invalid parameters count for Ctor \"" + StructClass.ClassName + "\"", ParsingContext);
            }
        }
    }
}
