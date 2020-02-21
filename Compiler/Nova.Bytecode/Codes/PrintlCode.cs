using Nova.ByteCode.Runtime;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class PrintlCode : ICode
    {
        public int TypeId => 10;

        public PrintlCode()
        {

        }
        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            Console.WriteLine(context.PopStack());
            index++;
        }
        public override string ToString()
        {
            return "Print";
        }

        public void Serialize(CppBinaryWriter writer)
        {
            
        }
    }
}
