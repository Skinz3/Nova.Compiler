using Nova.ByteCode.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    public interface IByteData
    {
        IByteElement GetByteElement(IByteElement parent);
    }
}
