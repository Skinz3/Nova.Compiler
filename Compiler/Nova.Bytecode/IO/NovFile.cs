using Nova.Bytecode.IO;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    public class NovFile : IByteElement
    {
        public const string HEADER = "NovaEX";

        public List<ByteClass> ByteClasses
        {
            get;
            private set;
        }
        public MainPointEntry MainPointEntry
        {
            get;
            set;
        }
        /// <summary>
        /// todo : referenced file (recursively)
        /// </summary>
        public NovFile()
        {
            this.ByteClasses = new List<ByteClass>();
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(HEADER);

            writer.Write(ByteClasses.Count);

            foreach (var pair in ByteClasses)
            {
                pair.Serialize(writer);
            }
        }
        public ByteMethod GetMainMethod()
        {
            return ByteClasses[MainPointEntry.ClassIndex].Methods[MainPointEntry.MethodsIndex];
        }
       
    }
}
