using Nova.Bytecode.Symbols;
using Nova.ByteCode.Codes;
using Nova.ByteCode.IO;
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
    public class ByteBlock : IByteElement
    {
        public SymbolTable SymbolTable
        {
            get;
            private set;
        }
        public List<ICode> Instructions
        {
            get;
            set;
        }
        public int ByteCodeLength
        {
            get
            {
                return Instructions.Count - 1;
            }
        }
        public int LocalsCount
        {
            get
            {
                return SymbolTable.Count;
            }
        }
        public ByteClass ParentClass
        {
            get;
            private set;
        }
        public ByteBlock(ByteClass parentClass)
        {
            this.SymbolTable = new SymbolTable();
            this.Instructions = new List<ICode>();
            this.ParentClass = parentClass;
        }

        public void Print()
        {
            foreach (var byteCode in Instructions)
            {
                Logger.Write(byteCode);
            }
        }

        public void Serialize(CppBinaryWriter writer)
        {
            //SymbolTable.Serialize(writer);

            int size = Instructions.Sum(x => x.GetSize() + 1);

            writer.Write(size);

            foreach (var code in Instructions)
            {
                writer.Write(code.OpId);
                code.Serialize(writer);
            }
        }

    }
}
