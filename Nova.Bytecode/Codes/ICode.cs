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
        int OpId
        {
            get;
        }

        void Serialize(CppBinaryWriter writer);

        int GetSize();
    }
}
