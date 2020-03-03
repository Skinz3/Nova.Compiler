using Nova.Bytecode.Runtime;
using Nova.ByteCode.Codes;
using Nova.ByteCode.Enums;
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
    public class ByteField : IByteElement
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
        public ModifiersEnum Modifiers
        {
            get;
            private set;
        }
        public ByteField(ByteClass parentClass, ModifiersEnum modifiers, string name, ByteBlockMetadata meta)
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
        public void Initializer(RuntimeContext context)
        {
            Exec.Execute(context, new object[0], Meta.Results);

            if (context.StackSize > 0)
                this.Value = context.PopStack();
        }
    }
}
