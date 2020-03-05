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
        public int OpId => 15;

        private int value;

        public PushIntCode(int value)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "PushInt " + value;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(value);
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
