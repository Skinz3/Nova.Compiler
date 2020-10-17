using Antlr4.Runtime;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Parser.Accessors;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions
{
    public class VariableNameExpression : Expression
    {
        private VariableAccessor Name
        {
            get;
            set;
        }
        public VariableNameExpression(IChild parent, ParserRuleContext context, string name) : base(parent, context)
        {
            this.Name = new VariableAccessor(name);
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
            Name.Validate(validator, this.Parent.ParentClass, ParsingContext);
        }
    }
}
