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
    public class MethodCallMemberCode : ICode
    {
        public int OpId => 9;

        private int methodId;

        public MethodCallMemberCode(int methodId)
        {
            this.methodId = methodId;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            context.Call(methodId);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(methodId);
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "MethodCall " + methodId;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
