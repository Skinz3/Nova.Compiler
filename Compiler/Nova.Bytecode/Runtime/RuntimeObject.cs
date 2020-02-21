using Nova.ByteCode.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Runtime
{
    public class RuntimeObject
    {
        private ByteClass Class
        {
            get;
            set;
        }
        private Dictionary<string, object> Properties
        {
            get;
            set;
        }

        public RuntimeObject(ByteClass @class)
        {
            this.Class = @class;

            foreach (var field in this.Class.Fields)
            {

            }
        }

    }
}
