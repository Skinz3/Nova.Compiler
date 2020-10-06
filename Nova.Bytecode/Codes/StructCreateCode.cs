using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class StructCreateCode : ICode
    {
        public int OpId => 23;

        private int classId;

        public StructCreateCode(int classId)
        {
            this.classId = classId;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "StructCreate " + classId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
