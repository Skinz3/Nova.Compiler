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
        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            object obj = context.PopStack();

            if (obj == null) // custom type for null values.
            {
                Console.WriteLine("NULL");
            }
            else
                Console.WriteLine(obj);


            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "Printl";
        }

        public void Serialize(CppBinaryWriter writer)
        {

        }
    }
}
