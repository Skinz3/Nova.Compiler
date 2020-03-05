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
    public class StructLoadMemberCode : ICode
    {
        public int OpId => 24;

        private int propertyId; // propertyId (symbolTable)

        public StructLoadMemberCode(int propertyId)
        {
            this.propertyId = propertyId;
        }

        public void Compute(RuntimeContext context, object[] locales, ref int index)
        {
            RuntimeStruct @struct = (RuntimeStruct)context.PopStack();
            context.PushStack(@struct.Get(propertyId));
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(propertyId);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "StructGetMemberCode " + propertyId;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
