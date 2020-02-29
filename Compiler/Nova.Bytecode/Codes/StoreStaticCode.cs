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
    public class StoreStaticCode : ICode
    {
        public int TypeId => 15;

        private string className;

        private string fieldName; // replace by id

        public StoreStaticCode(string className,string fieldName)
        {
            this.className = className;
            this.fieldName = fieldName;
        }


        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            object value = context.PopStack();
            context.Set(className, fieldName, value); 
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(fieldName);
        }
    }
}
