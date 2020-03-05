using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class SubCode : ICode
    {
        public int OpId => 27;

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            int val2 = (int)context.PopStack();
            int val1 = (int)context.PopStack();

            context.PushStack(val1 - val2);
            index++;
        }

        public int GetSize()
        {
            return 0;
        }

        public void Serialize(CppBinaryWriter writer)
        {

        }
    }
}
