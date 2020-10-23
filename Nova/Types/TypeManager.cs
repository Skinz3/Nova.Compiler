using Nova.IO;
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
        private Dictionary<string, NovaType> Types = new Dictionary<string, NovaType>();

        public TypeManager()
        {
        }


        public NovaType GetTypeInstance(string type)
        {
            NovaType result = null;
            Types.TryGetValue(type, out result);
            return result;
        }

        public void Register(Class @class, bool primitive)
        {
            NovaType type = new NovaType(@class, primitive);
            Types.Add(type.Name, type);
        }
    }
}
