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

        private int parametersCount;

        public MethodCallCode(int methodId, int parametersCount)
        {
            this.methodId = methodId;
            this.parametersCount = parametersCount;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.Call(methodId, parametersCount);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodId);
            writer.Write(parametersCount);
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "MethodCall " + methodId;
        }
    }
}
