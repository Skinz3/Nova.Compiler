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
        public int OpId => 26;

        private int propertyId;

        public StructStoreMemberCode(int propertyId)
        {
            this.propertyId = propertyId;
        }

        public void Compute(RuntimeContext context,object[] locales, ref int index)
        {
            RuntimeStruct @struct = (RuntimeStruct)context.PopStack();
            @struct.Set(propertyId, context.PopStack());
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(propertyId);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "StructSetMember " + propertyId;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
