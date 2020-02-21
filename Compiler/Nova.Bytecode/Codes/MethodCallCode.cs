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
        public int TypeId => 8;

        private string methodName;

        private int parametersCount;

        public MethodCallCode(string methodName, int parametersCount)
        {
            this.methodName = methodName;
            this.parametersCount = parametersCount;
        }

        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            context.Call(methodName, parametersCount);
            index++;
        }
        public override string ToString()
        {
            return "MethodCall " + methodName + " " + parametersCount;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodName);
            writer.Write(parametersCount);
        }
    }
}
