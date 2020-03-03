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
        private Dictionary<string, object> Properties
        {
            get;
            set;
        }

        public RuntimeStruct(ByteClass @class)
        {
            this.Class = @class;
            this.Properties = new Dictionary<string, object>();

            foreach (var field in Class.Fields) // modifiers != static
            {
                Properties.Add(field.Name, field.Value);
            }
        }

        public void Set(string property,object value)
        {
            this.Properties[property] = value;
        }
        public object Get(string property)
        {
            return this.Properties[property];
        }
        public override string ToString()
        {
            return "{" + Class.Name + "}";
        }
    }
}
