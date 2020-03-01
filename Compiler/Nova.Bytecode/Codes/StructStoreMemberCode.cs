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
    /*
     * this.Age = value
     */
    public class StructStoreMemberCode : ICode
    {
        public int TypeId => 21;

        private string propertyName;

        public StructStoreMemberCode(string property)
        {
            this.propertyName = property;
        }

        public void Compute(RuntimeContext context, ref object[] locales, ref int index)
        {
            RuntimeStruct @struct = (RuntimeStruct)context.PopStack();
            @struct.Set(propertyName, context.PopStack());
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(propertyName);
        }

        public override string ToString()
        {
            return "(" + TypeId + ") " + "StructSetMember " + propertyName;
        }
    }
}
