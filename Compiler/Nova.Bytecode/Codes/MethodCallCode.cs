using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class MethodCallCode : ICode
    {
        public int TypeId => 9;

        private int methodId;

        public MethodCallCode(int methodId)
        {
            this.methodId = methodId;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.Call(methodId);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodId);
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "MethodCall " + methodId;
        }
    }
}
