using Antlr4.Runtime;
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

namespace Nova.Parser.Accessors
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
        public abstract bool Validate(SemanticsValidator validator, Class parentClass, ParserRuleContext context);

        protected abstract SymbolType DeduceSymbolCategory(SemanticsValidator context, Class parentClass);
    }
}