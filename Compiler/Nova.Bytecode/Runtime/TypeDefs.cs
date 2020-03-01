using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Runtime
{
    public class TypeDefs
    {
        public static Null NULL_VALUE = new Null();
    }


    public struct Null
    {
        public override string ToString()
        {
            return "null";
        }
    }
}
