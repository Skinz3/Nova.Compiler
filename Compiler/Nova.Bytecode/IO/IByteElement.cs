using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    public interface IByteElement 
    {
        void Serialize(CppBinaryWriter writer);
        void Deserialize(CppBinaryReader reader);
    }
}
