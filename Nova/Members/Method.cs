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
using Nova.Bytecode.Enums;
using Antlr4.Runtime;
using Nova.Types;

namespace Nova.Members
{
    public class Method : IByteData, IChild, IAccessible
    {
        public Class ParentClass
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
            set;
        }
        public MethodType Type
        {
            get;
            set;
        }
        public string RawReturnType
        {
            get;
            set;
        }
        public NovaType ReturnType
        {
            get;
            set;
        }
        public List<Variable> Parameters
        {
            get;
            set;
        }
        public List<Statement> Statements
        {
            get;
            set;
        }
        private ParserRuleContext Context
        {
            get;
            set;
        }
        public IChild Parent => null;

        public Method(Class parentClass, int methodId, string methodName, ModifiersEnum modifiers, string returnType,
            List<Variable> parameters, ParserRuleContext context)
        {
            this.Id = methodId;
            this.ParentClass = parentClass;
            this.Name = methodName;
            this.Modifiers = modifiers;
            this.RawReturnType = returnType;
            this.Parameters = parameters;
            this.Context = context;
            this.Statements = new List<Statement>();
        }
        public bool IsMainPointEntry()
        {
            return Modifiers == ModifiersEnum.@public && Name == Constants.MainPointEntryMethodName && Parameters.Count == 0;
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
                result.ByteBlock.SymbolTable.Bind(parameter.Name, parameter.RawType);
            }
            foreach (var statement in Statements)
            {
                statement.GenerateBytecode(container, result.ByteBlock);
            }

            if (result.ByteBlock.Instructions.Count == 0 || (!(result.ByteBlock.Instructions.Last() is ReturnCode)))
            {
                result.ByteBlock.Instructions.Add(new ReturnCode());
            }


            return result;

        }

        public void ValidateSemantics(SemanticsValidator validator)
        {
            if (ParentClass.Type == ContainerType.@struct && IsMainPointEntry())
            {
                validator.AddError("Main point entry cannot be member of struct \"" + ParentClass.ClassName + "\"", Context);
            }
            if (ParentClass.Type == ContainerType.primitive && Parameters.Count == 0)
            {
                validator.AddError("Primitive method " + Name + " should have self has parameter.", Context);
            }
            foreach (var param in Parameters)
            {
                validator.DeclareVariable(param);
            }

            foreach (var statement in this.Statements)
            {
                statement.ValidateSemantics(validator);
            }
        }

        public void ValidateTypes(SemanticsValidator validator)
        {
            this.ReturnType = validator.Container.TypeManager.GetTypeInstance(RawReturnType);

            if (ReturnType == null)
            {
                validator.AddError("Unknown return type for method : " + Name, Context);
            }
            foreach (var parameter in Parameters)
            {
                parameter.ValidateTypes(validator);
            }

            foreach (var statement in Statements)
            {
                statement.ValidateTypes(validator);
            }
        }

        public Class GetContextualClass(SemanticsValidator validator)
        {
            return validator.Container.TryGetClass(RawReturnType);
        }
    }
}
