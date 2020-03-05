using Nova.ByteCode.Codes;
using Nova.ByteCode.Enums;
using Nova.ByteCode.Generation;
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
    public class ByteField : IByteElement
    {
        public string Name
        {
            get;
            set;
        }
        private ByteBlock Meta
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
        public ModifiersEnum Modifiers
        {
            get;
            private set;
        }
        public ByteField(ByteClass parentClass, ModifiersEnum modifiers, string name, ByteBlock meta)
        {
            this.ParentClass = parentClass;
            this.Name = name;
            this.Meta = meta;
            this.Modifiers = modifiers;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write((byte)Modifiers);
            Meta.Serialize(writer);
        }
      

        
    }
}
