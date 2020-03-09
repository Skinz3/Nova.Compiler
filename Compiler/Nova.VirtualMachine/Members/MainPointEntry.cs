using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Members
{
    public class MainPointEntry
    {
        public int ClassIndex
        {
            get;
            set;
        }
        public int MethodIndex
        {
            get;
            set;
        }
        public void Deserialize(CppBinaryReader reader)
        {
            this.ClassIndex = reader.ReadInt32();
            this.MethodIndex = reader.ReadInt32();
        }
    }
}
