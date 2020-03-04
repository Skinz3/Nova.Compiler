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
        public int TypeId => 15;

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
            return "(" + TypeId + ") " + "Readl";
        }
        public void Serialize(CppBinaryWriter writer)
        {
          
        }
    }
}
