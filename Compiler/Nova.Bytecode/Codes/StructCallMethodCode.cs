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
    public class StructCallMethodCode : ICode
    {
        public int TypeId => 19;

        private string methodName;

        private int parametersCount;

        public StructCallMethodCode(string methodName, int parametersCount)
        {
            this.methodName = methodName;
            this.parametersCount = parametersCount;
        }
        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            RuntimeStruct obj = (RuntimeStruct)context.PopStack();
            context.Call(obj, methodName, parametersCount);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructCallMethod " + methodName;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodName);
            writer.Write(parametersCount);
        }
    }
}
