using Nova.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Types
{
    public class NovaType
    {
        public bool IsPrimitive
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return Class.ClassName;
            }
        }

        public Class Class
        {
            get;
            set;
        }

        public NovaType(Class @class, bool isPrimitive)
        {
            this.Class = @class;
            this.IsPrimitive = isPrimitive;
        }

        public override string ToString()
        {
            return "NovaType {" + Name + "}";
        }

    }
}
