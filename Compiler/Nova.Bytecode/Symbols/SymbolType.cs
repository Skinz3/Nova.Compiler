using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Symbols
{
    public enum SymbolType
    {
        Local, // human (where human is a local variable)
        ClassMember, // human where human is a class field.
        StructMember, // human where human is a struct field
        StaticExternal, // Class.Field (public static)
        NoSymbol,
    }
}
