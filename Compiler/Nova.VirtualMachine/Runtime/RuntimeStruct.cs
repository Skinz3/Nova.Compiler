
using Nova.VirtualMachine.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Runtime
{
    public class RuntimeStruct
    {
        public ByteClass TypeClass
        {
            get;
            set;
        }
        public RuntimeStruct(ByteClass typeClass)
        {
            this.TypeClass = typeClass;
        }
    }
}
