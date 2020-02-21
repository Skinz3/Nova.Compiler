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

        public Dictionary<string, ByteClass> ByteClasses
        {
            get;
            private set;
        }
        /// <summary>
        /// todo : referenced file (recursively)
        /// </summary>
        public NovFile()
        {
            this.ByteClasses = new Dictionary<string, ByteClass>();
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(HEADER);

            writer.Write(ByteClasses.Count);

            foreach (var pair in ByteClasses)
            {
                writer.Write(pair.Key);
                pair.Value.Serialize(writer);
            }
        }

        
    }
}
