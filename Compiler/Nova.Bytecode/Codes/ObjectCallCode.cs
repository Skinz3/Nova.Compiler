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
    public class ObjectCallCode : ICode
    {
        public int TypeId => 19;

        private int variableId;

        private string methodName;

        private int parametersCount;

        public ObjectCallCode(int variableId, string methodName, int parametersCount)
        {
            this.variableId = variableId;
            this.methodName = methodName;
            this.parametersCount = parametersCount;
        }
        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeObject obj = (RuntimeObject)locals[variableId]; // todo
            context.Call(obj, methodName, parametersCount);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "ObjectCall " + variableId + " " + methodName;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(variableId);
            writer.Write(methodName);
        }
    }
}
