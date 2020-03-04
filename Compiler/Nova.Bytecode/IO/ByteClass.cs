using Nova.Bytecode.Symbols;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.IO
{
    public class ByteClass : IByteElement
    {
        public string Name
        {
            get;
            private set;
        }

        public List<ByteMethod> Methods
        {
            get;
            set;
        }

        public List<ByteField> Fields
        {
            get;
            set;
        }
        /*
         * Toutes les constantes de chaque blocs de toutes les methodes de la classe.
         */
        private List<object> ConstantsTable
        {
            get;
            set;
        }

        public ByteClass(string name)
        {
            this.Name = name;
            this.Methods = new List<ByteMethod>();
            this.Fields = new List<ByteField>();
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
            writer.Write(Name);

            writer.Write(Methods.Count);

            foreach (var method in Methods)
            {
                method.Serialize(writer);
            }

            writer.Write(Fields.Count);

            foreach (var field in Fields)
            {
                field.Serialize(writer);
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
    }
}
