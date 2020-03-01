using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.Statements
{
    public class ObjectAssignationStatement : Statement
    {
        public const string REGEX = @"^([a-zA-Z_$][a-zA-Z_$0-9]*)\s*=>\s*\((.*)\)$";

        private MemberName Target
        {
            get;
            set;
        }
        private StatementNode[] CtorParameters
        {
            get;
            set;
        }
        public ObjectAssignationStatement(IParentBlock parent, string input, int lineIndex, Match match) : base(parent, input, lineIndex)
        {
            this.Target = new MemberName(match.Groups[1].Value);

            string parametersStr = match.Groups[2].Value;

            this.CtorParameters = Parser.ParseMethodCallParameters(Parent, LineIndex, parametersStr);
        }

        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            var symInfo = DeduceSymbolCategory(context, Target, this.Parent.ParentClass);

            string type;

            Symbol symbol = context.SymbolTable.GetLocal(this.Target.GetRoot());

            if (symbol == null)
            {
                type = this.Parent.ParentClass.Fields[Target.GetRoot()].Type;
            }
            else
            {
                type = symbol.Type;
            }
            context.Results.Add(new StructCreateCode(type));

            switch (symInfo)
            {
                case SymbolType.Local:
                    context.Results.Add(new StoreCode(symbol.Id));
                    break;
                case SymbolType.ClassMember:
                    context.Results.Add(new StoreMemberCode(Target.Raw));
                    break;
                case SymbolType.StructMember:
                    context.Results.Add(new StructPushCurrent());
                    context.Results.Add(new StructStoreMemberCode(Target.GetRoot()));
                    break;
                case SymbolType.StaticExternal:
                    context.Results.Add(new StoreStaticCode(Target.GetRoot(), Target.Elements[1]));
                    break;
            }
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Target))
            {
                validator.AddError("Undefined reference to struct: " + Target.Raw, LineIndex);
            }
        }
    }
}
