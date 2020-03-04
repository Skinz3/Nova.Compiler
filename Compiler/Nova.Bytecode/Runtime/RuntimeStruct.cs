using Nova.ByteCode.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Runtime
{
    public class RuntimeStruct
    {
        public ByteClass Class
        {
            get;
            set;
        }
        private List<object> Properties
        {
            get;
            set;
        }

        public RuntimeStruct(ByteClass @class)
        {
            this.Class = @class;
            this.Properties = new List<object>();

            foreach (var field in Class.Fields) // modifiers != static
            {
                Properties.Add(field.Value);
            }
        }

        public void Set(int propertyId, object value)
        {
            this.Properties[propertyId] = value;
        }
        public object Get(int property)
        {
            return this.Properties[property];
        }
        public override string ToString()
        {
            return "{" + Class.Name + "}";
        }
    }
}
