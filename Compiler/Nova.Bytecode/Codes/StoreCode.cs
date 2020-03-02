using Nova.Bytecode.Runtime;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class StoreCode : ICode
    {
        public int TypeId => 14;

        private int variableId;

        public StoreCode(int variableId)
        {
            this.variableId = variableId;
        }

        public void Compute(RuntimeContext context, object[] locales, ref int index)
        {
            object value = context.PopStack();

            locales[variableId] = value;
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(variableId);
        }

        public override string ToString()
        {
            return "(" + TypeId + ") " + "Store " + variableId;
        }
    }
}
