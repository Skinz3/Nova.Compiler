using Nova.ByteCode.IO;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.IO
{
    public class MainPointEntry : IByteElement
    {
        public int ClassIndex
        {
            get;
            set;
        }
        public int MethodsIndex
        {
            get;
            set;
        }
        public MainPointEntry(int classIndex,int methodIndex)
        {
            this.ClassIndex = classIndex;
            this.MethodsIndex = methodIndex;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(ClassIndex);
            writer.Write(MethodsIndex);
        }
    }
}
