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
using Nova.Lexer.Accessors;

namespace Nova.Statements
{
    public class AssignationStatement : Statement
    {
        public static string REGEX = @"^([a-zA-Z_$][a-zA-Z_._$0-9]*)\s*(\+|-|\*|\/)?=\s*(.+)$";

        private VariableAccessor Target
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
            this.Target = new VariableAccessor(match.Groups[1].Value);

            if (match.Groups[2].Length > 0)
                this.Operator = match.Groups[2].Value[0];

            string valueStr = match.Groups[3].Value;

            this.Value = StatementTreeBuilder.Build(parent, valueStr, lineIndex);
        }
        public AssignationStatement(IParentBlock parent) : base(parent)
        {

        }


        public override void GenerateBytecode(ClassesContainer container, ByteBlockMetadata context)
        {
            Value.GenerateBytecode(container, context);
            GenerateAssignation(container, context, Target);
        }
        public static void GenerateAssignation(ClassesContainer container, ByteBlockMetadata context, VariableAccessor target)
        {

            int offset = 0;

            switch (target.Category)
            {
                case SymbolType.NoSymbol:
                    throw new NotImplementedException();
                case SymbolType.Local:

                    Variable variable = target.GetRoot<Variable>();
                    int variableId = context.SymbolTable.GetSymbol(variable.Name).Id;

                    if (target.NoTree())
                    {
                        context.Results.Add(new StoreCode(variableId));
                        return;
                    }
                    else
                    {
                        context.Results.Add(new LoadCode(variableId));
                        offset = 2;

                    }
                    break;
                case SymbolType.ClassMember:

                    Field targetField = target.GetRoot<Field>();

                    if (target.NoTree())
                    {
                        context.Results.Add(new StoreMemberCode(targetField.Id)); // field of a class.
                        return;
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticMemberCode(targetField.Id));
                        offset = 1;
                    }
                    break;
                case SymbolType.StructMember:
                    context.Results.Add(new StructPushCurrent());
                    offset = 0;

                    break;
                case SymbolType.StaticExternal:

                    Class owner = target.GetRoot<Class>();
                    targetField = target.GetLeaf<Field>();

                    if (target.Elements.Count == 2)
                    {
                        context.Results.Add(new StoreStaticCode(container.GetClassId(owner.ClassName), targetField.Id));
                        return;
                    }
                    else
                    {
                        context.Results.Add(new LoadStaticCode(container.GetClassId(owner.ClassName), targetField.Id));
                        offset = 2;
                    }
                    break;
            }
            Field field = null;

            for (int i = offset; i < target.Elements.Count - 1; i++)
            {
                field = target.GetElement<Field>(i);
                context.Results.Add(new StructLoadMemberCode(field.Id));
            }

            field = target.GetLeaf<Field>();

            context.Results.Add(new StructStoreMemberCode(field.Id));
        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            Target.Validate(validator, this.Parent.ParentClass, LineIndex);
            Value.ValidateSemantics(validator);
        }
    }
}
