using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Types
{
    public class BaseType : IType
    {
        public bool IsPrimitive => false;

        public string Name => throw new NotImplementedException();

        public Class GetClass()
        {
            throw new NotImplementedException();
        }
    }
}
