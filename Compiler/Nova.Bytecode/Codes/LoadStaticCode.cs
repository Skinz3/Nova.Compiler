using Nova.ByteCode;
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
    public class LoadStaticCode : ICode
    {
        public int TypeId => 6;

        private string className;

        private int fieldId;

        public LoadStaticCode(string className, int fieldId)
        {
            this.className = className;
            this.fieldId = fieldId;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.PushStack(context.Get(className, fieldId));
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "LoadStatic " + className+" "+ fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(fieldId);
        }
    }
}
