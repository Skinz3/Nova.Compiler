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
    public class ObjectCreateStoreCode : ICode
    {
        public int TypeId => 17;

        private string className;
        private int variableId;

        public ObjectCreateStoreCode(string className, int variableId)
        {
            this.className = className;
            this.variableId = variableId;
        }


        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            RuntimeObject obj = context.CreateObject(className);
            locals[variableId] = obj;
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "ObjectCreateStore " + className + " " + variableId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(className);
        }
    }
}
