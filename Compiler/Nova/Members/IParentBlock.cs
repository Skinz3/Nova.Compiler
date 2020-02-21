using Nova.Enums;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public interface IParentBlock
    {
        Class ParentClass
        {
            get;
        }
        IParentBlock Parent
        {
            get;
        }
    }
}
