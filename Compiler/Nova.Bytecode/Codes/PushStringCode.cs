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
    public class PushStringCode : ICode
    {
        public int TypeId => 24;

        private string value;

        public PushStringCode(string value)
        {
            this.value = value;
        }

        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            context.PushStack(value);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "PushString " + value;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(value);
        }
    }
}
