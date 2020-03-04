using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.Members;
using Nova.Semantics;
using Nova.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer.Accessors
{
    public abstract class Accessor
    {
        public string Raw
        {
            get;
            private set;
        }
        protected string[] ElementsStr // a retirer ?
        {
            get;
            private set;
        }
        public List<IAccessible> Elements
        {
            get;
            private set;
        }
        public SymbolType Category
        {
            get;
            protected set;
        }

        public bool NoTree()
        {
            return Elements.Count == 1;
        }
        public Accessor(string raw)
        {
            this.Raw = raw;
            this.ElementsStr = Raw.Split('.');
            Elements = new List<IAccessible>();
        }

        protected string GetRoot() // privé?
        {
            return ElementsStr[0];
        }
        protected string GetLeaf() // privé?
        {
            return ElementsStr[ElementsStr.Length - 1];
        }
        public override string ToString()
        {
            return Raw;
        }
        public T GetRoot<T>() where T : IAccessible
        {
            return (T)Elements[0];
        }
        public T GetLeaf<T>() where T : IAccessible
        {
            return (T)Elements[Elements.Count - 1];
        }
        public T GetElement<T>(int index) where T : IAccessible
        {
            return (T)Elements[index];
        }
        public abstract bool Validate(SemanticsValidator validator, Class parentClass, int lineIndex);

        protected SymbolType DeduceSymbolCategory(SemanticsValidator context, Class parentClass)
        {
            if (context.IsLocalDeclared(this.GetRoot()))
            {
                return SymbolType.Local;
            }
            else if (parentClass.Fields.ContainsKey(this.GetRoot()))
            {
                switch (parentClass.Type)
                {
                    case ContainerType.@class:
                        return SymbolType.ClassMember;
                    case ContainerType.@struct:
                        return SymbolType.StructMember;
                }

            }
            else
            {
                if (this.ElementsStr.Length == 1)
                {
                    return SymbolType.NoSymbol;
                }
                else
                {
                    return SymbolType.StaticExternal;
                }
            }

            throw new Exception("Unknown symbol type.");
        }
    }
}