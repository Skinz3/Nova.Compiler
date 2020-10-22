using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Members
{
    public interface IAccessible
    {
        public Class GetContextualClass(SemanticsValidator validator);
    }
}
