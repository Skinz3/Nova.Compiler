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
    public class LoadMemberCode : ICode
    {
        public int TypeId => 7;

        private string fieldName;

        public LoadMemberCode(string fieldName)
        {
            this.fieldName = fieldName;
        }


        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            context.PushStack(context.Get(fieldName));
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "LoadStaticMember " + fieldName;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(fieldName);
        }

    }
}
