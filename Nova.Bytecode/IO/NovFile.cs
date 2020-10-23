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
        private List<object> ConstantsTable
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
            this.ConstantsTable = new List<object>();
        }

        public object GetConstant(int constantId)
        {
            return ConstantsTable[constantId];
        }

        public int BindConstant(object constant)
        {
            ConstantsTable.Add(constant);
            return ConstantsTable.Count - 1;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(HEADER);

            MainPointEntry.Serialize(writer);

            writer.Write(ByteClasses.Count);

            foreach (var pair in ByteClasses)
            {
                pair.Serialize(writer);
            }

            writer.Write(ConstantsTable.Count);

            foreach (var value in ConstantsTable)
            {
                if (value is string)
                {
                    writer.Write(1);
                    writer.Write(value.ToString());
                }
                else if (value is bool)
                {
                    writer.Write(2);
                    writer.Write((bool)value);
                }
                else
                {
                    throw new Exception("Unhandled constant serialization.");
                }
            }
        }
        public ByteMethod GetMainMethod()
        {
            return ByteClasses[MainPointEntry.ClassIndex].Methods[MainPointEntry.MethodIndex];
        }

    }
}
