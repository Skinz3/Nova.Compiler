using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class ReadlCode : ICode
    {
        public int OpId => 17;

        public ReadlCode()
        {
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            string value = Console.ReadLine();
            context.PushStack(value);
            index++;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "Readl";
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
