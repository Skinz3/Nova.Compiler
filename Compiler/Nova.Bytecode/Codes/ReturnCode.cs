using Nova.Bytecode.Runtime;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class ReturnCode : ICode
    {
        public int TypeId =>16;

        public ReturnCode()
        {

        }
        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            index++;
        }
   
        public void Serialize(CppBinaryWriter writer)
        {
           
        }

    }
}
