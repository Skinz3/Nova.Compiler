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
        public int OpId => 5;

        public int targetIndex;

        public JumpCode(int targetIndex)
        {
            this.targetIndex = targetIndex;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(targetIndex);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "Jump " + targetIndex;
        }
        public int GetSize()
        {
            return 1;
        }
    }
}
