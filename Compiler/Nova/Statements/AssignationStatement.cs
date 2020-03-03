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

        private static void GenerateStructAssignation(MemberName target, ByteBlockMetadata context, int offset)
        {
            for (int i = offset; i < target.ElementsStr.Length - 1; i++)
            {
                context.Results.Add(new StructLoadMemberCode(target.ElementsStr[i]));
            }

            context.Results.Add(new StructStoreMemberCode(target.GetLeaf()));
        }
        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            Value.GenerateBytecode(container, context);
            var symInfo = this.DeduceSymbolCategory(context, Target, this.Parent.ParentClass);
            GenerateAssignation(context, Target, symInfo); 
        }
        public static void GenerateAssignation(ByteBlockMetadata context, MemberName target,SymbolType symInfo)
        {
           /* switch (symInfo)
            {
                case SymbolType.Local:

                    int variableId = context.SymbolTable.GetSymbol(target.GetRoot()).Id;

                    if (target.NoTree())
                    {
                        context.Results.Add(new StoreCode(variableId)); // field of a class.
                    }
                    else
                    {
                        context.Results.Add(new LoadCode(variableId));
                        GenerateStructAssignation(target, context, 1);

                    }
                    break;
                case SymbolType.ClassMember:

                    if (target.NoTree())
                    {
                        context.Results.Add(new StoreMemberCode(target.Raw)); // field of a class.
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticMemberCode(target.GetRoot()));
                        GenerateStructAssignation(target, context, 1);
                    }
                    break;
                case SymbolType.StructMember:
                    context.Results.Add(new StructPushCurrent()); // field of a struct
                    GenerateStructAssignation(target, context, 0);
                    break;
                case SymbolType.StaticExternal:

                    if (target.Elements.Length == 2)
                    {
                        context.Results.Add(new StoreStaticCode(target.Elements[0], target.Elements[1]));
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticCode(target.Elements[0], target.Elements[1]));
                        GenerateStructAssignation(target, context, 2);
                    }
                    break;
            } */
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
