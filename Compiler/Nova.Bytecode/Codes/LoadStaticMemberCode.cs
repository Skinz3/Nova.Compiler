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
    public class LoadStaticMemberCode : ICode
    {
        public int TypeId => 7; 

        private int fieldId; // fieldId (symbolTable)

        public LoadStaticMemberCode(int fieldId)
        {
            this.fieldId = fieldId;
        }


        public void Compute(RuntimeContext context,object[] locals, ref int index)
        {
            context.PushStack(context.Get(fieldId));
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "LoadStaticMember " + fieldId;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(fieldId);
        }

    }
}
