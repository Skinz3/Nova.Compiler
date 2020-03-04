using Nova.Bytecode.Runtime;
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
        public int TypeId => 19;

        private string className;

        private int fieldId; 

        public StoreStaticCode(string className,int fieldId)
        {
            this.className = className;
            this.fieldId = fieldId;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            object value = context.PopStack();
            context.Set(className, fieldId, value); 
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StoreStaticCode " + className +" " + fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
            writer.Write(fieldId);
        }
    }
}
