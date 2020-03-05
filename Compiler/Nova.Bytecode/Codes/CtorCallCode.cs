using Nova.ByteCode.Codes;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Codes
{
    public class CtorCallCode : ICode
    {
        public int OpId => 3;

        private int parametersCount;

        private int methodId;

        public CtorCallCode(int methodId, int parametersCount)
        {
            this.methodId = methodId;
            this.parametersCount = parametersCount;
        }
        public override string ToString()
        {
            return "(" + OpId + ") " + "CtorCall " + parametersCount;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(parametersCount);
            writer.Write(methodId);
        }
        public int GetSize()
        {
            return 2;
        }
    }
}
