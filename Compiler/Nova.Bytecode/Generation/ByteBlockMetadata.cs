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
        private Dictionary<string, int> LocalsRelator
        {
            get;
            set;
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
                return LocalsRelator.Count;
            }
        }
        public ByteBlockMetadata()
        {
            this.LocalsRelator = new Dictionary<string, int>();
            this.Results = new List<ICode>();
        }

        public int BindVariable(string name)
        {
            int id = (LocalsRelator.Count - 1) + 1;
            LocalsRelator.Add(name, id);
            return id;
        }
        public int GetLocalVariableId(string name)
        {
            int id = 0;

            if (LocalsRelator.TryGetValue(name, out id))
            {
                return id;
            }
            else
            {
                return -1;
            }
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
            writer.Write(LocalsRelator.Count);

            foreach (var relator in LocalsRelator)
            {
                writer.Write(relator.Key);
                writer.Write(relator.Value);
            }

            writer.Write(Results.Count);

            foreach (var code in Results)
            {
                writer.Write(code.TypeId);
                code.Serialize(writer);
            }
        }
    }
}
