using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Enums
{
    [Flags]
    public enum ModifiersEnum
    {
        @public = 0x1,
        @private = 0x2,
        @static = 0x4,
    }
}
