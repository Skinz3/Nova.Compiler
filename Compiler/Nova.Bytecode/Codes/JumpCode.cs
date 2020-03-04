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
    public class JumpCode : ICode
    {
        public int TypeId => 4;

        public int targetIndex;

        public JumpCode(int targetIndex)
        {
            this.targetIndex = targetIndex;
        }

        public void Compute(RuntimeContext context, object[] locals, ref int index)
        {
            index = targetIndex;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(targetIndex);
        }

        public override string ToString()
        {
            return "(" + TypeId + ") " + "Jump " + targetIndex;
        }
    }
}
