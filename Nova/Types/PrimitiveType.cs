using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Types
{
    public abstract class PrimitiveType : IType
    {
        public bool IsPrimitive => true;

        public abstract string Name
        {
            get;
        }

        public abstract Class GetClass();
    }
}
