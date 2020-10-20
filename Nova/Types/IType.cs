using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Types
{
    public interface IType
    {
        Class GetClass();

        bool IsPrimitive
        {
            get;
        }
        string Name
        {
            get;
        }


    }
}
