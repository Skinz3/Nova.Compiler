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
        public int TypeId => 16;

        private string fieldName;

        public StoreMemberCode(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            object value = context.PopStack();
            context.Set(fieldName, value);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(fieldName);
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "StoreMember " + fieldName;
        }
    }
}
