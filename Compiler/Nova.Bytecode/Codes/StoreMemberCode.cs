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
    public class StoreMemberCode : ICode
    {
        public int TypeId => 18;

        private int fieldId;

        public StoreMemberCode(int fieldId)
        {
            this.fieldId = fieldId;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            object value = context.PopStack();
            context.Set(fieldId, value);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(fieldId);
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StoreMember " + fieldId;
        }
    }
}
