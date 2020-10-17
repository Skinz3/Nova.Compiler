using Nova.ByteCode.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Parser
{
    class ParserUtils
    {
        public static ModifiersEnum ParseModifier(string modifier)
        {
            return (ModifiersEnum)Enum.Parse(typeof(ModifiersEnum), modifier);
        }
    }
}
