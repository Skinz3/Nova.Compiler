using Nova.ByteCode.Codes;
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
        public int OpId => 14;

        private int constantId;

        public PushConstCode(int constantId)
        {
            this.constantId = constantId;
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
