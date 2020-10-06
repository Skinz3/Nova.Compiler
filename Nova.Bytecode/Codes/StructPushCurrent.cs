using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    /*
     * human.Name = "value"
     */
    public class StructPushCurrent : ICode
    {
        public int OpId => 25;

        public StructPushCurrent()
        {

        }

        public void Serialize(CppBinaryWriter writer)
        {
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "StructPushCurrent";
        }
        public int GetSize()
        {
            return 0;
        }
    }
}
