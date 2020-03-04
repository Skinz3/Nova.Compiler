using Nova.Bytecode.Runtime;
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
    public class PushNullCode : ICode
    {
        public int OpId => 14;

        public PushNullCode()
        {

        }

        public void Compute(RuntimeContext context,object[] locals, ref int index)
        {
            context.PushStack(TypeDefs.NULL_VALUE);
            index++;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "PushNull";
        }

        public void Serialize(CppBinaryWriter writer)
        {
          
        }
        public int GetSize()
        {
            return 0;
        }
    }
}
