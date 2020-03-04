using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Utils.IO
{
    public class CppBinaryReader : BinaryReader
    {
        public CppBinaryReader(Stream input) : base(input)
        {
        }

        public CppBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public CppBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        public override string ReadString()
        {
            string result = string.Empty;

            char read = this.ReadChar();

            while (read != '\0')
            {
                result += read;
                read = this.ReadChar();
            }
            return result;
        }
       
    }
}
