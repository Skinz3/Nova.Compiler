using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    public enum UsingType
    {
        Ref,
        Std,
    }
    public class Using
    {
        public UsingType Type
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        public Using(UsingType type,string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
