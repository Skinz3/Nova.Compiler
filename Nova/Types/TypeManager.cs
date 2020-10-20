using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Types
{
    public class TypeManager
    {
        private Dictionary<string, IType> Types = new Dictionary<string, IType>();

        public IType GetTypeInstance(string type)
        {
            throw new NotImplementedException();
        }

        public void Register(Class @class)
        {

        }


    }
}
