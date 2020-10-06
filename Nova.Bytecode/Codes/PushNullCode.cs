using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class PushNullCode : ICode
    {
        public int OpId => 16;

        public PushNullCode()
        {

        }
     
        public override string ToString()
        {
            return "(" + OpId + ") " + "PushNull";
        }

        public void Serialize(CppBinaryWriter writer)
        {
          
        }
        public int GetSize()
        {
            return 0;
        }
    }
}
