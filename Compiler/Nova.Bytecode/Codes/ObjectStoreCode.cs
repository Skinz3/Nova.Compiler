using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class ObjectStoreCode : ICode
    {
        public int TypeId => 20;

        private int variableId;

        private string propertyName;

        public ObjectStoreCode(int variableId,string propertyName)
        {
            this.variableId = variableId;
            this.propertyName = propertyName;
        }
        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeObject obj = (RuntimeObject)locals[variableId];
            obj.Set(propertyName, context.PopStack());
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(variableId);
            writer.Write(propertyName);
        }
    }
}
