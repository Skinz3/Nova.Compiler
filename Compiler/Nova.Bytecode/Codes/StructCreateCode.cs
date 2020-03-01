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
    public class StructCreateCode : ICode
    {
        public int TypeId => 17;

        private string className;

        public StructCreateCode(string className)
        {
            this.className = className;
        }

        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeStruct obj = context.CreateObject(className);
            context.PushStack(obj);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructCreate " + className;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
        }
    }
}
