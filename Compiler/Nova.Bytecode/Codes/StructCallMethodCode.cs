using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class StructCallMethodCode : ICode
    {
        public int OpId => 22;

        private int methodId;

        public StructCallMethodCode(int methodId)
        {
            this.methodId = methodId;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "StructCallMethod " + methodId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodId);
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
