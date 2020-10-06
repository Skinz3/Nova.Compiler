using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.Lexer.Accessors;

namespace Nova.Statements
{
    public class VariableNameStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s*$";

        private VariableAccessor Name
        {
            get;
            set;
        }
        public VariableNameStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
            this.Name = new VariableAccessor(input);
        }

        public VariableNameStatement(IParentBlock parent) : base(parent)
        {

        }


        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            int loadStart = 0;

            switch (this.Name.Category)
            {
                case SymbolType.NoSymbol:
                    throw new NotImplementedException();
                case SymbolType.Local:

                    Variable variable = Name.GetRoot<Variable>();
                    Symbol symbol = context.SymbolTable.GetSymbol(variable.Name);
                    context.Instructions.Add(new LoadCode(symbol.Id));
                    loadStart = 1;

                    break;
                case SymbolType.ClassMember:

                    Field target = Name.GetRoot<Field>();
                    context.Instructions.Add(new LoadGlobalCode(container.GetClassId(this.Parent.ParentClass.ClassName), target.Id));
                    loadStart = 1;

                    break;
                case SymbolType.StructMember:

                    context.Instructions.Add(new StructPushCurrent());
                    loadStart = 0;

                    break;
                case SymbolType.StaticExternal:
                    target = this.Name.GetElement<Field>(1);
                    context.Instructions.Add(new LoadGlobalCode(container.GetClassId(Name.GetRoot<Class>().ClassName), target.Id));
                    loadStart = 2;
                    break;
            }

            Field field = null;

            for (int i = loadStart; i < Name.Elements.Count; i++)
            {
                field = this.Name.GetElement<Field>(i);
                context.Instructions.Add(new StructLoadMemberCode(field.Id));
            }


        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Name.Validate(validator, this.Parent.ParentClass, LineIndex);

        }


    }
}
