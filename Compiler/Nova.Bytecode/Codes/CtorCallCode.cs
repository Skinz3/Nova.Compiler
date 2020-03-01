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
    public class CtorCallCode : ICode
    {
        public int TypeId => 23;

        private int parametersCount;

        public CtorCallCode(int parametersCount)
        {
            this.parametersCount = parametersCount;
        }
        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeStruct obj = (RuntimeStruct)context.StackMinus(parametersCount);
            context.Call(obj, obj.Class.Name, parametersCount);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "CtorCall " + parametersCount;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(parametersCount);
        }
    }
}
