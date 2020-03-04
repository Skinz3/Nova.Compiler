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
        private MainPointEntry MainMetadata
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
        public ByteMethod GetMainEntryPoint()
        {
            return ByteClasses[MainMetadata.ClassIndex].Methods[MainMetadata.MethodsIndex];
        }
        public bool ComputeEntryPoint() // rien a faire ici?
        {
            int i = 0;
            foreach (var @class in ByteClasses)
            {
                int j = 0;
                foreach (var @method in @class.Methods)
                {
                    if (method.IsMainPointEntry())
                    {
                        if (MainMetadata == null)
                        {
                            this.MainMetadata = new MainPointEntry(i, j);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    j++;
                }
                i++;
            }

            return true;
        }
    }
}
