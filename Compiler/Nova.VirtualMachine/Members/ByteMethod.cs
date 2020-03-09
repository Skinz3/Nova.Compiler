using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Members
{
    public class ByteMethod
    {
        public string Name
        {
            get;
            private set;
        }
        public ByteClass Parent
        {
            get;
            private set;
        }
        public Modifiers Modifiers
        {
            get;
            private set;
        }
        public int ParametersCount
        {
            get;
            private set;
        }
        public ByteBlock Block
        {
            get;
            private set;
        }
        public ByteMethod(ByteClass parent)
        {
            this.Parent = parent;
            this.Block = new ByteBlock();
        }
        public void Deserialize(CppBinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.Modifiers = (Modifiers)reader.ReadByte();
            this.ParametersCount = reader.ReadInt32();

            this.Block.Deserialize(reader);
        }
    }
}
