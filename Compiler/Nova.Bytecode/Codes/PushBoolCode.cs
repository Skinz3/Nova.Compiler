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
    public class PushBoolCode : ICode
    {
        public int TypeId => 26;

        private bool value;

        public PushBoolCode(bool value)
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
            return "(" + TypeId + ") " + "PushBool " + value;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(value);
        }
    }
}
