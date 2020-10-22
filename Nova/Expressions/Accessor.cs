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

namespace Nova.Expressions
{
    public class Accessor
    {
        public SymbolType InferredSymbolType
        {
            get;
            set;
        }
        private Expression Expression
        {
            get;
            set;
        }
        private IAccessible Target
        {
            get;
            set;
        }
        public string Identifier
        {
            get
            {
                return ((VariableNameExpression)Expression).Name;
            }
        }
        public Accessor(Expression expression)
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

        public void GenerateBytecode(ClassesContainer container, ByteBlock context)
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
                case SymbolType.Static:

                    //    target = this.Name.GetElement<Field>(1);
                    //   context.Instructions.Add(new LoadGlobalCode(container.GetClassId(Name.GetRoot<Class>().ClassName), target.Id));
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return Expression.ToString();
        }

        public static List<Accessor> BuildAccessorTree(VariableNameExpression root)
        {
            var tree = new List<Accessor>();

            tree.Insert(0, root.CreateAccessor());

            Expression currentExpr = root.AccessorExpression;

            while (currentExpr != null)
            {
                var expr = ((VariableNameExpression)currentExpr);

                tree.Insert(0, expr.CreateAccessor());
                currentExpr = expr.AccessorExpression;
            }

            return tree;
        }
        public static void ValidateAccessorTree(VariableNameExpression root, SemanticsValidator validator)
        {
            int i = 0;

            Accessor current = null;
            Accessor previous = null;

            while (i < root.Tree.Count)
            {
                current = root.Tree[i];

                if (previous == null)
                {
                    if (validator.IsLocalDeclared(current.Identifier))
                    {
                        current.InferredSymbolType = SymbolType.Local;
                        current.SetTarget(validator.GetLocal(current.Identifier));
                    }
                    else if (root.ParentClass.Fields.ContainsKey(current.Identifier))
                    {
                        switch (root.ParentClass.Type)
                        {
                            case ContainerType.@class:
                                current.InferredSymbolType = SymbolType.ClassMember;
                                current.SetTarget(root.ParentClass.Fields[current.Identifier]);
                                break;
                            case ContainerType.@struct:
                                current.InferredSymbolType = SymbolType.StructMember;
                                current.SetTarget(root.ParentClass.Fields[current.Identifier]);
                                break;
                            default:
                                throw new NotImplementedException("Unable to infer symbol category");
                        }
                    }
                    else if (validator.Container.ContainsClass(current.Identifier))
                    {
                        current.InferredSymbolType = SymbolType.Static;
                        current.SetTarget(validator.Container[current.Identifier]);
                    }
                    else
                    {
                        current.InferredSymbolType = SymbolType.Unknown;
                        validator.AddError("Unknown reference to member : " + current.Identifier, root.ParsingContext);
                    }
                }
                else
                {
                    Class targetClass = null;

                    if (previous.InferredSymbolType == SymbolType.Local)
                    {
                        Variable target = previous.GetTarget<Variable>();
                        targetClass = validator.Container.TryGetClass(target.Type); // parent class null ?  develop types !
                    }
                    else if (previous.InferredSymbolType == SymbolType.StructMember || previous.InferredSymbolType == SymbolType.ClassMember)
                    {
                        Field target = previous.GetTarget<Field>();
                        targetClass = validator.Container.TryGetClass(target.Type);
                    }
                    else if (previous.InferredSymbolType == SymbolType.Static)
                    {
                        targetClass = previous.GetTarget<Class>();
                    }

                    if (targetClass.Fields.ContainsKey(current.Identifier))
                    {
                        if (targetClass.Type == ContainerType.@class)
                        {
                            current.InferredSymbolType = SymbolType.ClassMember;
                        }
                        else if (targetClass.Type == ContainerType.@struct)
                        {
                            current.InferredSymbolType = SymbolType.StructMember;
                        }
                        else
                        {
                            throw new NotImplementedException(); // todo : records
                        }

                        current.SetTarget(targetClass.Fields[current.Identifier]);

                    }
                    else
                    {
                        validator.AddError("Unknown reference to member : " + current.Identifier, root.ParsingContext);
                    }
                }


                previous = current;
                i++;

            }
        }

    }
}
