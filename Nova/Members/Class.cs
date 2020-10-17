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
using Nova.Lexer.Accessors;
using Nova.Utils;

namespace Nova.Members
{
    public class Class : IAccessible
    {
        public const string CLASS_PATTERN = @"^\s*(class|struct)\s+(\w+)$";

        public string ClassName
        {
            get;
            private set;
        }
        public NvFile File
        {
            get;
            private set;
        }
        private int StartIndex
        {
            get;
            set;
        }
        private int EndIndex
        {
            get;
            set;
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

        public Class(NvFile file, string className, ContainerType type, int startIndex, int endIndex)
        {
            this.File = file;
            this.ClassName = className;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Methods = new Dictionary<string, Method>();
            this.Fields = new Dictionary<string, Field>();
            this.Type = type;
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
            return Methods.Values.FirstOrDefault(x => x.Modifiers == ModifiersEnum.ctor);
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
            List<SemanticalError> errors = new List<SemanticalError>();

            foreach (var field in this.Fields)
            {
                errors.AddRange(field.Value.ValidateSemantics(container));
            }
            foreach (var method in this.Methods)
            {
                errors.AddRange(method.Value.ValidateSemantics(container));
            }

            return errors;
        }
    }
}
