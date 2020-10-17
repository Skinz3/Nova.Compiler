using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.Lexer;
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
        private VariableAccessor Target
        {
            get;
            set;
        }
        /// <summary>
        /// null -> '='
        /// </summary>
        private char Operator
        {
            get;
            set;
        }
        private StatementNode Value
        {
            get;
            set;
        }
        public AssignationStatement(IChild parent, VariableAccessor target, char op, StatementNode node, string line, int lineIndex) : base(parent, line, lineIndex)
        {
            this.Target = target;
            this.Operator = op;
            this.Value = node;
        }
        public AssignationStatement(IChild parent) : base(parent)
        {

        }


        public override void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            Value.GenerateBytecode(container, context);
            GenerateAssignation(container, context, Target);
        }
        public static void GenerateAssignation(ClassesContainer container, ByteBlock context, VariableAccessor target)
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
                        context.Instructions.Add(new StoreCode(variableId));
                        return;
                    }
                    else
                    {
                        context.Instructions.Add(new LoadCode(variableId));
                        offset = 1;

                    }
                    break;
                case SymbolType.ClassMember:

                    Field targetField = target.GetRoot<Field>();

                    if (target.NoTree())
                    {
                        context.Instructions.Add(new StoreGlobalCode(container.GetClassId(context.ParentClass.Name), targetField.Id)); // field of a class.
                        return;
                    }
                    else
                    {
                        context.Instructions.Add(new LoadGlobalCode(container.GetClassId(context.ParentClass.Name), targetField.Id));
                        offset = 1;
                    }
                    break;
                case SymbolType.StructMember:
                    context.Instructions.Add(new StructPushCurrent());
                    offset = 0;

                    break;
                case SymbolType.StaticExternal:

                    Class owner = target.GetRoot<Class>();
                    targetField = target.GetLeaf<Field>();

                    if (target.Elements.Count == 2)
                    {
                        context.Instructions.Add(new StoreGlobalCode(container.GetClassId(owner.ClassName), targetField.Id));
                        return;
                    }
                    else
                    {
                        context.Instructions.Add(new LoadGlobalCode(container.GetClassId(owner.ClassName), targetField.Id));
                        offset = 2;
                    }
                    break;
            }
            Field field = null;

            for (int i = offset; i < target.Elements.Count - 1; i++)
            {
                field = target.GetElement<Field>(i);
                context.Instructions.Add(new StructLoadMemberCode(field.Id));
            }

            field = target.GetLeaf<Field>();

            context.Instructions.Add(new StructStoreMemberCode(field.Id));
        }
        public override void ValidateSemantics(SemanticsValidator validator)
        {
            if (!Target.Validate(validator, this.Parent.ParentClass, LineIndex))
            {
                return;
            }
            Value.ValidateSemantics(validator);
        }
    }
}
