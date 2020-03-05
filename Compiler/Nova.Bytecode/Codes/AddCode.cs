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
    public class AddCode : ICode
    {
        public int OpId => 1;

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.PushStack((int)context.PopStack() + (int)context.PopStack());
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
