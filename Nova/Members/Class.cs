using Nova.IO;
using Nova.Lexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.ByteCode.IO;
using Nova.Semantics;
using Nova.ByteCode.Enums;
using Nova.Bytecode.Enums;
using Nova.Utils;
using Nova.Types;
using Antlr4.Runtime;

namespace Nova.Members
{
    public class Class : IAccessible
    {
        public string ClassName
        {
            get;
            set;
        }
        public NvFile File
        {
            get;
            private set;
        }
        public ContainerType Type
        {
            get;
            private set;
        }
        public Dictionary<string, Method> Methods
        {
            get;
            private set;
        }
        public Dictionary<string, Field> Fields
        {
            get;
            private set;
        }
        private ParserRuleContext Context
        {
            get;
            set;
        }

        public Class(NvFile file, string className, ContainerType type, ParserRuleContext context)
        {
            this.File = file;
            this.ClassName = className;
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
            this.Type = type;
            this.Context = context;
        }

        public Class()
        {
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
        }


        public override string ToString()
        {
            return ClassName;
        }
        public Method GetCtor()
        {
            return Methods.Values.FirstOrDefault(x => x.Type == MethodType.Ctor);
        }
        public int PopMethodId()
        {
            return Methods.Count;
        }
        public int PopFieldId()
        {
            return Fields.Count;
        }
        public IByteElement GetByteElement(NovFile file, ClassesContainer container, IByteElement parent)
        {
            ByteClass byteClass = new ByteClass(file, this.ClassName, Type);

            foreach (var method in this.Methods)
            {
                byteClass.Methods.Add((ByteMethod)method.Value.GetByteElement(container, byteClass));
            }


            foreach (var field in this.Fields)
            {
                byteClass.Fields.Add((ByteField)field.Value.GetByteElement(container, byteClass));
            }

            return byteClass;

        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(this, container);

            foreach (var field in this.Fields)
            {
                field.Value.ValidateSemantics(validator);
                validator.Flush();
            }
            foreach (var method in this.Methods)
            {
                method.Value.ValidateSemantics(validator);
                validator.Flush();
            }

            return validator.GetErrors();
        }

        public Class GetContextualClass(SemanticsValidator validator)
        {
            return this;
        }

        public IEnumerable<SemanticalError> ValidateTypes(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(this, container);

            foreach (var field in this.Fields)
            {
                field.Value.ValidateTypes(validator);
                validator.Flush();
            }

            foreach (var method in this.Methods)
            {
                method.Value.ValidateTypes(validator);
                validator.Flush();
            }

            return validator.GetErrors();
        }
    }
}
