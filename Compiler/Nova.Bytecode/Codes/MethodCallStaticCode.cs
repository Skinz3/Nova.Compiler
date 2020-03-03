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
        private int methodId;
        private int parametersCount;

        public MethodCallStaticCode(string className, int methodId, int parametersCount)
        {
            this.className = className;
            this.methodId = methodId;
            this.parametersCount = parametersCount;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.Call(className, methodId, parametersCount);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "MethodCallStatic " + className + " " + methodId + " " + parametersCount;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(methodId);
            writer.Write(parametersCount);
        }

    }
}
