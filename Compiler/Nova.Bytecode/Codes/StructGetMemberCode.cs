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
    public class StructGetMemberCode : ICode
    {
        public int TypeId => 22;

        private string propertyName;

        public StructGetMemberCode(string property)
        {
            this.propertyName = property;
        }

        public void Compute(RuntimeContext context, ref object[] locales, ref int index)
        {
            RuntimeStruct @struct = context.StructsStack.Peek();
            context.PushStack(@struct.Get(propertyName));
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(propertyName);
        }

        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructGetMemberCode " + propertyName;
        }
    }
}
