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
    public class NativeCallCode : ICode
    {
        public int OpId => 13;

        private int nativeId;

        public NativeCallCode(int nativeId)
        {
            this.nativeId = nativeId;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "NativeId " + nativeId;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(nativeId);
        }

        public int GetSize()
        {
            return 1;
        }
    }
}
