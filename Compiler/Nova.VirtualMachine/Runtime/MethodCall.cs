using Nova.VirtualMachine.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Runtime
{
    public class MethodCall
    {
        public ByteMethod Method
        {
            get;
            set;
        }
        public int ReturnIp
        {
            get;
            set;
        }
        public object[] PreviousLocales
        {
            get;
            set;
        }
        public ByteMethod PreviousMethod
        {
            get;
            set;
        }
        public MethodCall(ByteMethod method, ByteMethod previousMethod, int returnIp, object[] previousLocales)
        {
            this.Method = method;
            this.ReturnIp = returnIp;
            this.PreviousMethod = previousMethod;
            this.PreviousLocales = previousLocales;
        }
    }
}
