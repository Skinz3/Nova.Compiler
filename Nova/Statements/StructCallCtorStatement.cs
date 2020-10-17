using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private StatementNode[] Parameters
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
        public StructCallCtorStatement(IChild parent) : base(parent)
        {
        }

        public StructCallCtorStatement(IChild parent, string line, int lineIndex, string name, StatementNode[] parameters) : base(parent, line, lineIndex)
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

                context.Instructions.Add(new CtorCallCode(targetClass.GetCtor().Id, Parameters.Length));
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
                validator.AddError("Unknown struct type " + CtorName, LineIndex);
            }

            this.StructClass = validator.Container[CtorName];

            this.StructCtor = StructClass.GetCtor();

            if (Parameters.Length > 0)
            {
                if (StructCtor == null)
                {
                    validator.AddError("Unknown struct ctor \"" + StructClass.ClassName + "\"", LineIndex);
                }
                else if (Parameters.Length != StructCtor.Parameters.Count)
                {
                    validator.AddError("Invalid parameters count for Ctor \"" + StructClass.ClassName + "\"", LineIndex);
                }
            }

        }
    }
}
