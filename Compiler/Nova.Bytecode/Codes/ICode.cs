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
    public interface ICode
    {
        int TypeId
        {
            get;
        }
        void Compute(RuntimeContext context, ref object[] locals, ref int index);
        void Serialize(CppBinaryWriter writer);
    }
}
