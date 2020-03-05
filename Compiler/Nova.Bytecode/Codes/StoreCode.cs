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
        public int OpId => 19;

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
            return "(" + OpId + ") " + "Store " + variableId;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
