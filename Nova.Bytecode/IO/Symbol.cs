using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Bytecode.IO
{
    public class Symbol
    {
        public int Id;

        public Symbol(int id)
        {
            this.Id = id;
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Id);
        }
    }
    public class SymbolTable
    {
        private Dictionary<string, Symbol> Symbols
        {
            get;
            set;
        }

        public SymbolTable()
        {
            this.Symbols = new Dictionary<string, Symbol>();
        }

        public int BindVariable(string name)
        {
            int id = (Symbols.Count - 1) + 1;
            Symbols.Add(name, new Symbol(id));
            return id;
        }
        public int GetVariableId(string name)
        {
            Symbol sym = null;

            if (Symbols.TryGetValue(name, out sym))
            {
                return sym.Id;
            }
            else
            {
                return -1;
            }
        }
        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write(Symbols.Count);

            foreach (var pair in Symbols)
            {
                writer.Write(pair.Key);
                pair.Value.Serialize(writer);
            }
        }


    }
}
