using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Members
{
    public class ByteBlock
    {
        public int LocalesCount
        {
            get;
            set;
        }
        public List<int> Instructions
        {
            get;
            set;
        }
        public ByteBlock()
        {
            this.Instructions = new List<int>();
        }
        public void Deserialize(CppBinaryReader reader)
        {
            int size = reader.ReadInt32();

            for (int i = 0; i < size; i++)
            {
                this.Instructions.Add(reader.ReadInt32());
            }
            this.LocalesCount = reader.ReadInt32();
        }
    }
}
