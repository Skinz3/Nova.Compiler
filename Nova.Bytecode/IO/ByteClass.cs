using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    public class ByteClass : IByteElement
    {
        public string Name
        {
            get;
            private set;
        }
        public ContainerType Type
        {
            get;
            private set;
        }
        public List<ByteMethod> Methods
        {
            get;
            set;
        }

        public List<ByteField> Fields
        {
            get;
            set;
        }

        public NovFile NovFile
        {
            get;
            private set;
        }

        public ByteClass(NovFile file, string name, ContainerType type)
        {
            this.NovFile = file;
            this.Name = name;
            this.Type = type;
            this.Methods = new List<ByteMethod>();
            this.Fields = new List<ByteField>();
        }


        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Name);

            writer.Write((byte)Type);

            writer.Write(Methods.Count);

            foreach (var method in Methods)
            {
                method.Serialize(writer);
            }

            writer.Write(Fields.Count);

            foreach (var field in Fields)
            {
                field.Serialize(writer);
            }
        }
    }
}
