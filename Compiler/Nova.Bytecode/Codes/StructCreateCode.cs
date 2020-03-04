using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
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
        public int TypeId => 21;

        private int classId;

        public StructCreateCode(int classId)
        {
            this.classId = classId;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            RuntimeStruct obj = context.CreateObject(classId);
            context.PushStack(obj);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructCreate " + classId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
        }
    }
}
