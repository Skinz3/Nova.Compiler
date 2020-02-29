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

        private string fieldName;

        public LoadStaticCode(string className,string fieldName)
        {
            this.className = className;
            this.fieldName = fieldName;
        }


        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            context.PushStack(context.Get(className, fieldName));
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "LoadStatic " + className+" "+fieldName;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(fieldName);
        }
    }
}
