using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    /// <summary>
    /// Field static
    /// </summary>
    public class ByteStaticField : IByteElement
    {
        public string Name
        {
            get;
            set;
        }
        private ByteBlockMetadata Meta
        {
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
        public ByteClass ParentClass
        {
            get;
            set;
        }
        public ByteStaticField(ByteClass parentClass, string name, ByteBlockMetadata meta)
        {
            this.ParentClass = parentClass;
            this.Name = name;
            this.Meta = meta;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            Meta.Serialize(writer);
        }

     
        public void Initializer(RuntimeContext context)
        {
            Exec.Execute(context, new object[0], Meta.Results);
            this.Value = context.PopStack();
        }
    }
}
