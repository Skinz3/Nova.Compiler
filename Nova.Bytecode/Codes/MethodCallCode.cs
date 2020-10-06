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
    public class MethodCallCode : ICode
    {
        public int OpId => 11;

        private int classId;
        private int methodId;

        public MethodCallCode(int classId, int methodId)
        {
            this.classId = classId;
            this.methodId = methodId;
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "MethodCall " + classId + " " + methodId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
            writer.Write(methodId);
        }
        public int GetSize()
        {
            return 2;
        }
    }
}
