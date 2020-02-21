using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Utils.IO
{
    public class CppBinaryWriter : BinaryWriter
    {
        public CppBinaryWriter(Stream output) : base(output)
        {
        }

        public CppBinaryWriter(Stream output, Encoding encoding) : base(output, encoding)
        {
        }

        public CppBinaryWriter(Stream output, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen)
        {
        }

        protected CppBinaryWriter()
        {
        }

        public override void Write(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                this.Write(value[i]);
            }
            this.Write('\0');
        }
    }
}
