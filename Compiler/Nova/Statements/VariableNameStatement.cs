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

namespace Nova.Statements
{
    public class VariableNameStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s*$";

        private MemberName Name
        {
            get;
            set;
        }
        public VariableNameStatement(IParentBlock parent, string input, int lineIndex) : base(parent, input, lineIndex)
        {
            this.Name = new MemberName(input);
        }

        public VariableNameStatement(IParentBlock parent) : base(parent)
        {

        }

        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            var symType = DeduceSymbolCategory(context, Name, this.Parent.ParentClass);

            int loadStart = 1;

            switch (symType)
            {
                case SymbolType.Local:
                    context.Results.Add(new LoadCode(context.SymbolTable.GetLocal(Name.GetRoot()).Id));
                    
                    break;
                case SymbolType.ClassMember:
                    context.Results.Add(new LoadStaticMemberCode(Name.GetRoot()));
                    break;
                case SymbolType.StructMember:
                    context.Results.Add(new StructPushCurrent());
                    loadStart = 0;
                    break;
                case SymbolType.StaticExternal:
                    context.Results.Add(new LoadStaticCode(Name.GetRoot(), Name.Elements[1]));
                    loadStart = 2;
                    break;
            }

            for (int i = loadStart; i < Name.Elements.Length; i++)
            {
                context.Results.Add(new StructLoadMemberCode(Name.Elements[i]));
            }

        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Name))
            {
                validator.AddError("Undefined reference to \"" + Name.Raw + "\"", LineIndex);
            }
        }

    }
}
