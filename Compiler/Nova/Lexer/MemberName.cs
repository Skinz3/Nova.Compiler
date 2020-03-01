using Nova.Members;
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

        public string[] Elements
        {
            get;
            private set;
        }

        public bool NoTree()
        {
            return Elements.Length == 1;
        }
        public MemberName(string raw)
        {
            this.Raw = raw;
            this.Elements = Raw.Split('.');
        }
        public string GetRoot()
        {
            return Elements[0];
        }
        public string GetLeaf()
        {
            return Elements[Elements.Length - 1];
        }
        public override string ToString()
        {
            return Raw;
        }

    }
}