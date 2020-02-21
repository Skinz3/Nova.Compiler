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
        public int TypeId => 9;

        private string className;
        private string methodName;
        private int parametersCount;

        public MethodCallStaticCode(string className, string methodName, int parametersCount)
        {
            this.className = className;
            this.methodName = methodName;
            this.parametersCount = parametersCount;
        }


        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            context.Call(className, methodName, parametersCount);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(methodName);
            writer.Write(parametersCount);
        }

        public override string ToString()
        {
            return "StaticMethodCall " + methodName + " " + parametersCount;
        }
    }
}
