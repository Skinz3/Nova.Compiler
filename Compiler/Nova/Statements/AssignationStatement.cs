using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
using Nova.Lexer.Tokens;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;

namespace Nova.Statements
{
    public class AssignationStatement : Statement
    {
        public static string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\s*(\+|-|\*|\/)?=\s*(.+)$";

        private MemberName Target
        {
            get;
            set;
        }
        /// <summary>
        /// null -> '='
        /// </summary>
        private char? Operator
        {
            get;
            set;
        }
        private StatementNode Value
        {
            get;
            set;
        }
        public AssignationStatement(IParentBlock parent, string line, int lineIndex, Match match) : base(parent, line, lineIndex)
        {
            this.Target = new MemberName(match.Groups[1].Value);

            if (match.Groups[2].Length > 0)
                this.Operator = match.Groups[2].Value[0];

            string valueStr = match.Groups[3].Value;

            this.Value = StatementTreeBuilder.Build(parent, valueStr, lineIndex);
        }
        public AssignationStatement(IParentBlock parent) : base(parent)
        {

        }


        public override void GenerateBytecode(ByteBlockMetadata context)
        {
            Value.GenerateBytecode(context);

            var symInfo = this.DeduceSymbolCategory(context, Target, this.Parent.ParentClass);

            switch (symInfo)
            {
                case SymbolType.Local:
                    context.Results.Add(new StoreCode(context.SymbolTable.GetLocal(Target.Raw).Id)); // local variable
                    break;
                case SymbolType.ClassMember:

                    if (Target.NoTree())
                    {
                        context.Results.Add(new StoreMemberCode(Target.Raw)); // field of a class.
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticMemberCode(Target.GetRoot()));

                        for (int i = 1; i < Target.Elements.Length - 1; i++)
                        {
                            context.Results.Add(new StructLoadMemberCode(Target.Elements[i]));
                        }

                        context.Results.Add(new StructStoreMemberCode(Target.GetLeaf()));
                    }
                    break;
                case SymbolType.StructMember:
                    context.Results.Add(new StructPushCurrent()); // field of a struct
                    break;
                case SymbolType.StaticExternal:
                    context.Results.Add(new StoreStaticCode(Target.Elements[0], Target.Elements[1]));
                    break;
            }
        }

        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!validator.IsVariableDeclared(this.Parent.ParentClass, Target))
            {
                validator.AddError("Undefined reference to \"" + Target + "\"", LineIndex);
            }
            Value.ValidateSemantics(validator);
        }
    }
}
