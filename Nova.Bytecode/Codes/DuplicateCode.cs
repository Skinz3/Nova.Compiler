using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class DuplicateCode : ICode
    {
        public int OpId => 29;

        public int GetSize()
        {
            return 0;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            
        }
    }
}
