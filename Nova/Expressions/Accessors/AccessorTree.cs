using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Expressions.Accessors
{
    public class AccessorTree
    {
        private List<Accessor> Tree
        {
            get;
            set;
        }
        private AccessorExpression Root
        {
            get;
            set;
        }
        private bool Store
        {
            get;
            set;
        }

        public AccessorTree(AccessorExpression root, bool store)
        {
            this.Root = root;
            this.Store = store;
            this.BuildTree();
        }
        private void BuildTree()
        {
            Tree = new List<Accessor>();

            Tree.Insert(0, new Accessor(Root));

            AccessorExpression currentExpr = Root.ParentAccessor;

            while (currentExpr != null)
            {
                Tree.Insert(0, new Accessor(currentExpr));
                currentExpr = currentExpr.ParentAccessor; // name?
            }
        }
        public void ValidateSemantics(SemanticsValidator validator)
        {
            int i = 0;

            Accessor previous = null;

            while (i < Tree.Count)
            {
                Accessor current = Tree[i];

                if (previous == null)
                {
                    if (current.Type == AccessorType.Field)
                    {
                        if (validator.IsLocalDeclared(current.Identifier))
                        {
                            current.InferredSymbolType = SymbolType.Local;
                            current.SetTarget(validator.GetLocal(current.Identifier));
                        }
                        else if (Root.ParentClass.Fields.ContainsKey(current.Identifier))
                        {
                            switch (Root.ParentClass.Type)
                            {
                                case ContainerType.@class:
                                    current.InferredSymbolType = SymbolType.ClassMember;
                                    current.SetTarget(Root.ParentClass.Fields[current.Identifier]);
                                    break;
                                case ContainerType.@struct:
                                    current.InferredSymbolType = SymbolType.StructMember;
                                    current.SetTarget(Root.ParentClass.Fields[current.Identifier]);
                                    break;
                                default:
                                    throw new NotImplementedException("Unable to infer symbol category");
                            }
                        }
                        else if (validator.Container.ContainsClass(current.Identifier))
                        {
                            current.InferredSymbolType = SymbolType.StaticClass;
                            current.SetTarget(validator.Container[current.Identifier]);
                        }
                        else
                        {
                            current.InferredSymbolType = SymbolType.Unknown;
                            validator.AddError("Unknown reference to member : " + current.Identifier, Root.ParsingContext);
                        }
                    }
                    else if (current.Type == AccessorType.Method)
                    {
                        if (Root.ParentClass.Methods.ContainsKey(current.Identifier))
                        {
                            switch (Root.ParentClass.Type)
                            {
                                case ContainerType.@class:
                                    current.InferredSymbolType = SymbolType.ClassMember;
                                    current.SetTarget(Root.ParentClass.Methods[current.Identifier]);
                                    break;
                                case ContainerType.@struct:
                                    current.InferredSymbolType = SymbolType.StructMember;
                                    current.SetTarget(Root.ParentClass.Methods[current.Identifier]);
                                    break;
                                default:
                                    throw new NotImplementedException("Unable to infer symbol category");
                            }
                        }
                        else
                        {
                            current.InferredSymbolType = SymbolType.Unknown;
                            validator.AddError("Unknown reference to member : " + current.Identifier, Root.ParsingContext);
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown accessor type : " + current.Type);
                    }
                }
                else
                {
                    Class targetClass = previous.GetTargetClass(validator);

                    if (targetClass == null)
                    {
                        validator.AddError("Types are not implemented", Root.ParsingContext);
                        return;
                    }

                    if (current.Type == AccessorType.Field)
                    {
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
                            validator.AddError("Unknown reference to member : " + current.Identifier, Root.ParsingContext);
                        }
                    }
                    else if (current.Type == AccessorType.Method)
                    {
                        if (targetClass.Methods.ContainsKey(current.Identifier))
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

                            current.SetTarget(targetClass.Methods[current.Identifier]);

                        }
                        else
                        {
                            validator.AddError("Unknown reference to member : " + current.Identifier, Root.ParsingContext);
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown accessor type : " + current.Type);
                    }
                }


                previous = current;
                i++;

            }
        }

        public void GenerateBytecode(ClassesContainer container, ByteBlock context)
        {
            for (int i = 0; i < Tree.Count - 1; i++)
            {
                Tree[i].GenerateLoadBytecode(container, context);
            }

            if (Store)
            {
                Tree.Last().GenerateStoreBytecode(container, context);
            }
            else
            {
                Tree.Last().GenerateLoadBytecode(container, context);
            }
        }
    }
}
