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
    public class PushConstCode : ICode
    {
        public int OpId => 12;

        private int constantId;

        public PushConstCode(int constantId)
        {
            this.constantId = constantId;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.PushStack(context.GetConstant(constantId));
            index++;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "PushConst " + constantId;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(constantId);
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
