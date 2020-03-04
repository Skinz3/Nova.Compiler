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
    public class PushIntCode : ICode
    {
        public int TypeId => 13;

        private int value;

        public PushIntCode(int value)
        {
            this.value = value;
        }

        public void Compute(RuntimeContext context,  object[] locals, ref int index)
        {
            context.PushStack(value);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "PushInt " + value;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(value);
        }
    }
}
