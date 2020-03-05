using Nova.ByteCode;
using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class LoadStaticCode : ICode
    {
        public int OpId => 8;

        private int classId;

        private int fieldId;

        public LoadStaticCode(int classId, int fieldId)
        {
            this.classId = classId;
            this.fieldId = fieldId;
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "LoadStatic " + classId + " " + fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
            writer.Write(fieldId);
        }
        public int GetSize()
        {
            return 2;
        }
    }
}
