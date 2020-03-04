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

        private int classId;

        private int fieldId; 

        public StoreStaticCode(int classId,int fieldId)
        {
            this.classId = classId;
            this.fieldId = fieldId;
        }


        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            object value = context.PopStack();
            context.Set(classId, fieldId, value); 
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StoreStaticCode " + classId + " " + fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(classId);
            writer.Write(fieldId);
        }
    }
}
