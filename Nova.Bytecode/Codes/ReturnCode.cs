using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class ReturnCode : ICode
    {
        public int OpId => 18;

        public ReturnCode()
        {

        }
        public void Serialize(CppBinaryWriter writer)
        {

        }
        public int GetSize()
        {
            return 0;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "Return";
        }
    }
}
