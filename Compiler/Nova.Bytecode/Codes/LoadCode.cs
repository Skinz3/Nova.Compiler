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
    public class LoadCode : ICode
    {
        public int TypeId => 5;

        private int variableId;

        public LoadCode(int variableId)
        {
            this.variableId = variableId;
        }

        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            context.PushStack(locals[variableId]);
            index++;
        }
        public override string ToString()
        {
            return "Load " + variableId;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(variableId);
        }
    }
}
