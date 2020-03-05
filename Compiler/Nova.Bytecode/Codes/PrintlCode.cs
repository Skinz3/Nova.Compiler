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
        public int OpId => 13;

        public PrintlCode()
        {

        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "Printl";
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
