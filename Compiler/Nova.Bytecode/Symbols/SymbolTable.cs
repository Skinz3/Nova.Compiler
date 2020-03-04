using Nova.ByteCode.IO;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.Symbols
{
    public class SymbolTable : IByteElement
    {
        private Dictionary<string, Symbol> m_table;

        public int Count
        {
            get
            {
                return m_table.Count;
            }
        }
        public SymbolTable()
        {
            this.m_table = new Dictionary<string, Symbol>();
        }
        public Symbol GetSymbol(string name)
        {
            Symbol sym = null;

            if (m_table.TryGetValue(name, out sym))
            {
                return sym;
            }
            else
            {
                return null;
            }
        }
        public int Bind(string name,string type)
        {
            int id = (m_table.Count - 1) + 1;
            m_table.Add(name, new Symbol(id, type));
            return id;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(m_table.Count);

            foreach (var symbol in m_table)
            {
                writer.Write(symbol.Key);
                symbol.Value.Serialize(writer);
            }
        }

        public void Deserialize(CppBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
