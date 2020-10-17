using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class StructCallCtorStatement : Statement
    {
        private string CtorName
        {
            get;
            set;
        }
        private List<ExpressionNode> Parameters
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

        public StructCallCtorStatement(IChild parent, string name, List<ExpressionNode> parameters, ParserRuleContext context) : base(parent, context)
        {
            this.CtorName = name;
            this.Parameters = parameters;
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

            this.StructClass = validator.Container[CtorName];

            this.StructCtor = StructClass.GetCtor();

            if (Parameters.Count > 0)
            {
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
}
