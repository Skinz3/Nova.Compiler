using Nova.Enums;
using Nova.IO;
using Nova.Semantics;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public class Interface
    {
        public const string INTERFACE_PATTERN = @"^\s*interface\s+([a-zA-Z_$][a-zA-Z_$0-9]*)$";

        public Class ParentClass => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public ModifiersEnum Modifiers => throw new NotImplementedException();

        public IParentBlock Parent => throw new NotImplementedException();
    }
}
