using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.Lexer.Tokens;
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

namespace Nova.Members
{
    public class Field : IParentBlock, IByteData
    {
        public const string FIELD_PATTERN = @"^(public|private)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\s+([a-zA-Z_$][a-zA-Z_$0-9]*)\s*(=\s*(.*))?$";

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
        public string Type
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
        private StatementNode Value
        {
            get;
            set;
        }
        private string ValueStr
        {
            get;
            set;
        }
        private int LineIndex
        {
            get;
            set;
        }

        public IParentBlock Parent => null;

        public Field(Class parentClass, int fieldId, ModifiersEnum modifiers, Variable variable, string valueStr, int lineIndex)
        {
            this.Id = fieldId;
            this.ParentClass = parentClass;
            this.Modifiers = modifiers;
            this.Variable = variable;
            this.ValueStr = valueStr;
            this.LineIndex = lineIndex;
        }
        public Field()
        {

        }
        public bool Build()
        {
            this.Value = StatementTreeBuilder.Build(this, ValueStr, LineIndex);
            return true;
        }
        public override string ToString()
        {
            return Variable.Type + " " + Variable.Name;
        }

        public IByteElement GetByteElement(ClassesContainer container, IByteElement parent)
        {
            ByteBlockMetadata meta = new ByteBlockMetadata((ByteClass)parent);
            Value.GenerateBytecode(container, meta);
            ByteField field = new ByteField((ByteClass)parent, Modifiers, Name, meta);
            return field;
        }

        public IEnumerable<SemanticalError> ValidateSemantics(ClassesContainer container)
        {
            SemanticsValidator validator = new SemanticsValidator(container);
            Value.ValidateSemantics(validator);
            return validator.GetErrors();
        }
    }
}
