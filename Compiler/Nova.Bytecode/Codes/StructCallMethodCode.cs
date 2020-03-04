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
        public int TypeId => 20;

        private int methodId;

        public StructCallMethodCode(int methodId)
        {
            this.methodId = methodId;
        }
        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            RuntimeStruct obj = (RuntimeStruct)context.PopStack();
            context.Call(obj, methodId);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructCallMethod " + methodId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodId);
        }
    }
}
