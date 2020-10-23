using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class LoadClassCode : ICode
    {
        public int OpId => 29;

        private int classId;

        public LoadClassCode(int classId)
        {
            this.classId = classId;
        }

        public int GetSize()
        {
            return 1;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
        }
    }
}
