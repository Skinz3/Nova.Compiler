using Nova.Bytecode.Symbols;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Lexer
{
    public class MemberName
    {
        public string Raw
        {
            get;
            private set;
        }
        public string[] ElementsStr
        {
            get;
            private set;
        }


        public bool NoTree()
        {
            return ElementsStr.Length == 1;
        }
        public MemberName(string raw)
        {
            this.Raw = raw;
            this.ElementsStr = Raw.Split('.');
        }

        public string GetRoot()
        {
            return ElementsStr[0];
        }
        public string GetLeaf()
        {
            return ElementsStr[ElementsStr.Length - 1];
        }
        public override string ToString()
        {
            return Raw;
        }

    }
}