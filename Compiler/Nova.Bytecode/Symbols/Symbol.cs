using Nova.ByteCode.IO;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Symbols
{
    public class Symbol : IByteElement 
    {
        public int Id;
        public string Type;

        public Symbol(int id,string type)
        {
            this.Id = id;
            this.Type = type;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Type);
        }
        public override string ToString()
        {
            return "{" + Id + ": " + Type + "}";
        }
    }
}
