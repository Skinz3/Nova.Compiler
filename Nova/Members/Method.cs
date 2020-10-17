using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.IO;
using Nova.Semantics;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.ByteCode.Enums;
using Nova.Lexer.Accessors;

namespace Nova.Members
{
    public class Method : IChild, IByteData, IAccessible, IStatementBlock
    {
        public Class ParentClass
        {
            get;
            private set;
        }
        private int StartIndex
        {
            get;
            set;
        }
        public int EndIndex
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            set;
        }
        public int Id
        {
            get;
            set;
        }
        public ModifiersEnum Modifiers
        {
            get;
            private set;
        }
        public string ReturnType
        {
            get;
            private set;
        }
        public List<Variable> Parameters
        {
            get;
            private set;
        }
        public List<Statement> Statements
        {
            get;
            private set;
        }

        public IChild Parent => null;

        public Method(Class parentClass, int methodId, string methodName, ModifiersEnum modifiers, string returnType, List<Variable> parameters, int startIndex, int endIndex,
            List<Statement> statements)
        {
            this.Id = methodId;
            this.ParentClass = parentClass;
            this.Name = methodName;
            this.Modifiers = modifiers;
            this.ReturnType = returnType;
            this.Parameters = parameters;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Statements = statements;
        }
        public Method(Class parentClass)
        {
            this.ParentClass = parentClass;
            this.Statements = new List<Statement>();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ParentClass + "." + Name);
            sb.Append("(");
            sb.Append(string.Join(",", Parameters.Select(x => x.ToString())));
            sb.Append(")");

            return sb.ToString();
        }

        public IByteElement GetByteElement(ClassesContainer container, IByteElement parent)
        {
            ByteMethod result = new ByteMethod(this.Name, Modifiers, Parameters.Count, (ByteClass)parent);

            foreach (var parameter in Parameters)
            {
                result.Meta.SymbolTable.Bind(parameter.Name, parameter.Type);
            }
            foreach (var statement in Statements)
            {
                statement.GenerateBytecode(container, result.Meta);
            }

            if (result.Meta.Instructions.Count == 0 || (!(result.Meta.Instructions.Last() is ReturnCode)))
            {
                result.Meta.Instructions.Add(new ReturnCode());
            }


            return result;

        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(ParentClass, container);

            foreach (var param in Parameters)
            {
                validator.DeclareVariable(param.Name, param.Type);
            }

            foreach (var statement in this.Statements)
            {
                statement.ValidateSemantics(validator);
            }

            return validator.GetErrors();
        }
    }
}
