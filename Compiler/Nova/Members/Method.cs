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
    public class Method : IParentBlock, IByteData, IAccessible
    {
        public const string METHOD_PATTERN = @"^(public|private)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\((.*?)\)";

        public const string CTOR_PATTERN = @"^=>\s*([a-zA-Z_$][a-zA-Z_$0-9]*)\s*\((.*?)\)";

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

        public IParentBlock Parent => null;

        public Method(Class parentClass, int methodId, string methodName, ModifiersEnum modifiers, string returnType, List<Variable> parameters, int startIndex, int endIndex)
        {
            this.Id = methodId;
            this.ParentClass = parentClass;
            this.Name = methodName;
            this.Modifiers = modifiers;
            this.ReturnType = returnType;
            this.Parameters = parameters;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Statements = new List<Statement>();
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

        public bool BuildStatements()
        {
            this.Statements = Parser.BuildStatementBlock(this, StartIndex, EndIndex, this.ParentClass.File.Lines);
            return true;
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


            return result;

        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(container);

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
