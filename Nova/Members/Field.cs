using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.IO;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Semantics;
using Nova.ByteCode.Enums;
using Antlr4.Runtime;

namespace Nova.Members
{
    public class Field : IChild, IByteData , IAccessible
    {
        public Class ParentClass
        {
            get;
            private set;
        }
        public string Name
        {
            get
            {
                return Variable.Name;
            }
        }
        public int Id
        {
            get;
            set;
        }
        public string Type /* Validate type is a struct. Cannot be class ! */
        {
            get
            {
                return Variable.Type;
            }
        }
        public ModifiersEnum Modifiers
        {
            get;
            private set;
        }
        public Variable Variable
        {
            get;
            private set;
        }
        public ExpressionNode Value
        {
            get;
            set;
        }

        public IChild Parent => null;

        public Field(Class parentClass, int fieldId, ModifiersEnum modifiers, Variable variable)
        {
            this.Id = fieldId;
            this.ParentClass = parentClass;
            this.Modifiers = modifiers;
            this.Variable = variable;
        }
        public Field()
        {

        }

        public override string ToString()
        {
            return Modifiers + " " + Variable.Type + " " + Variable.Name;
        }

        public IByteElement GetByteElement(ClassesContainer container, IByteElement parent)
        {
            ByteBlock meta = new ByteBlock((ByteClass)parent);
            Value.GenerateBytecode(container, meta);
            ByteField field = new ByteField((ByteClass)parent, Modifiers, Name, meta);
            return field;
        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(ParentClass, container);
            Value.ValidateSemantics(validator);
            return validator.GetErrors();
        }

        public Class GetContextualClass(SemanticsValidator validator)
        {
            return validator.Container.TryGetClass(Type);
        }
    }
}
