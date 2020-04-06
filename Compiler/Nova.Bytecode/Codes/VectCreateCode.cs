using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class VectCreateCode : ICode
    {
        public int OpId => 28;

        private int size;

        public VectCreateCode(int size)
        {
            this.size = size;
        }

        public int GetSize()
        {
            return 1;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(size);
        }
    }
}
