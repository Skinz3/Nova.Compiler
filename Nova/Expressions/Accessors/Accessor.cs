using Nova.Bytecode.Codes;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions.Accessors
{
    public class Accessor
    {
        public SymbolType InferredSymbolType
        {
            get;
            set;
        }
        private AccessorExpression Expression
        {
            get;
            set;
        }
        private IAccessible Target
        {
            get;
            set;
        }
        public AccessorType Type
        {
            get
            {
                return Expression.AccessorType;
            }
        }
        public string Identifier
        {
            get
            {
                return Expression.Name;
            }
        }
        public Accessor(AccessorExpression expression)
        {
            this.Expression = expression;
        }

        public T GetTarget<T>() where T : IAccessible
        {
            return (T)Target;
        }
        public void SetTarget(IAccessible target)
        {
            this.Target = target;
        }

        public void GenerateStoreBytecode(ClassesContainer container, ByteBlock context)
        {
            switch (InferredSymbolType)
            {
                case SymbolType.Local:
                    Variable variable = GetTarget<Variable>();
                    Symbol symbol = context.SymbolTable.GetSymbol(variable.Name);
                    context.Instructions.Add(new LoadCode(symbol.Id));
                    break;
                case SymbolType.ClassMember:
                    Field target = GetTarget<Field>();
                    context.Instructions.Add(new StoreGlobalCode(container.GetClassId(target.ParentClass), target.Id));
                    break;
                case SymbolType.StructMember:
                    target = GetTarget<Field>();

                    if (Expression.ParentClass == target.ParentClass)
                    {
                        context.Instructions.Add(new StructPushCurrent());
                    }

                    context.Instructions.Add(new StructStoreMemberCode(target.Id));
                    break;
                case SymbolType.ExternalMember:
                    throw new NotImplementedException();
                default:
                    break;
            }
        }
        public void GenerateLoadBytecode(ClassesContainer container, ByteBlock context)
        {
            if (Type == AccessorType.Field)
            {
                switch (InferredSymbolType)
                {
                    case SymbolType.Local:
                        Variable variable = GetTarget<Variable>();
                        Symbol symbol = context.SymbolTable.GetSymbol(variable.Name);
                        context.Instructions.Add(new LoadCode(symbol.Id));
                        break;
                    case SymbolType.ClassMember:
                        Field target = GetTarget<Field>();
                        context.Instructions.Add(new LoadGlobalCode(container.GetClassId(target.ParentClass.ClassName), target.Id));
                        break;
                    case SymbolType.StructMember:
                        target = GetTarget<Field>();

                        if (Expression.ParentClass == target.ParentClass)
                        {
                            context.Instructions.Add(new StructPushCurrent());
                        }

                        context.Instructions.Add(new StructLoadMemberCode(target.Id));
                        break;
                    case SymbolType.ExternalMember:
                        target = GetTarget<Field>();
                        context.Instructions.Add(new LoadGlobalCode(container.GetClassId(target.ParentClass.ClassName), target.Id));
                        break;
                    default:
                        break;
                }
            }
            else if (Type == AccessorType.Method)
            {
                Method target = GetTarget<Method>();

                switch (InferredSymbolType)
                {
                    case SymbolType.ClassMember:
                        context.Instructions.Add(new MethodCallCode(container.GetClassId(target.ParentClass), target.Id));
                        break;
                    case SymbolType.StructMember:

                        if (Expression.ParentClass == target.ParentClass)
                        {
                            context.Instructions.Add(new StructPushCurrent());
                        }

                        context.Instructions.Add(new StructCallMethodCode(target.Id));
                        break;
                    case SymbolType.ExternalMember:
                        context.Instructions.Add(new MethodCallCode(container.GetClassId(target.ParentClass), target.Id));
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
        public override string ToString()
        {
            return Expression.ToString();
        }

        public Class GetTargetClass(SemanticsValidator validator)
        {
            return Target.GetContextualClass(validator);
        }
    }
}
