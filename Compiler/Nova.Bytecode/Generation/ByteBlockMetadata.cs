using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Generation
{
    public class ByteBlockMetadata
    {
        public SymbolTable SymbolTable
        {
            get;
            private set;
        }
        public List<ICode> Results
        {
            get;
            set;
        }
        public int ByteCodeLength
        {
            get
            {
                return Results.Count - 1;
            }
        }
        public int LocalsCount
        {
            get
            {
                return SymbolTable.Count;
            }
        }

        public ByteBlockMetadata()
        {
            this.SymbolTable = new SymbolTable();
            this.Results = new List<ICode>();
        }
        public void Print()
        {
            foreach (var byteCode in Results)
            {
                Logger.Write(byteCode);
            }
        }

        public void Serialize(CppBinaryWriter writer)
        {
            SymbolTable.Serialize(writer);
          

            writer.Write(Results.Count);

            foreach (var code in Results)
            {
                writer.Write(code.TypeId);
                code.Serialize(writer);
            }
        }
    }
}
