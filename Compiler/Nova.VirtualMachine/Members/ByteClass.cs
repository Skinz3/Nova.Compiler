using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Members
{
    public class ByteClass
    {
        private string Name
        {
            get;
            set;
        }
        public List<ByteMethod> Methods
        {
            get;
            private set;
        }
        public List<ByteField> Fields
        {
            get;
            private set;
        }
        public List<object> Constants
        {
            get;
            private set;
        }
        public ByteClass()
        {
            this.Methods = new List<ByteMethod>();
            this.Fields = new List<ByteField>();
            this.Constants = new List<object>();
        }
        public void Deserialize(CppBinaryReader reader)
        {
            this.Name = reader.ReadString();

            int methodsCount = reader.ReadInt32();

            for (int i = 0; i < methodsCount; i++)
            {
                ByteMethod method = new ByteMethod(this);
                method.Deserialize(reader);
                Methods.Add(method);
            }

            int fieldsCount = reader.ReadInt32();

            for (int i = 0; i < fieldsCount; i++)
            {
                ByteField field = new ByteField();
                field.Deserialize(reader);
                Fields.Add(field);
            }

            int constantsCount = reader.ReadInt32();
            for (int i = 0; i < constantsCount; i++)
            {
                int type = reader.ReadInt32();

                switch (type)
                {
                    case 1:
                        Constants.Add(reader.ReadString());
                        break;
                    case 2:
                        Constants.Add(reader.ReadBoolean());
                        break;
                    default:
                        throw new Exception("Unknown constant type.");
                }
            }
        }
    }
}
