using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class MethodCallStaticCode : ICode
    {
        public int TypeId => 10;

        private int classId;
        private int methodId;

        public MethodCallStaticCode(int classId, int methodId)
        {
            this.classId = classId;
            this.methodId = methodId;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.Call(classId, methodId);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "MethodCallStatic " + classId + " " + methodId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
            writer.Write(methodId);
        }

    }
}
