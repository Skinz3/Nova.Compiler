using Nova.Bytecode.Runtime;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Codes
{
    public class CreateConstCode : ICode
    {
        private static Dictionary<Type, byte> TypeLinks = new Dictionary<Type, byte>() // to complete
        {
            {typeof(bool),1 },
            {typeof(int),2 },
            {typeof(string),3 },
        };

        public int TypeId => 11;

        private object value;

        public CreateConstCode(object value)
        {
            this.value = value;
        }

        public void Compute(RuntimeContext context, ref object[] locals, ref int index)
        {
            context.PushStack(value);
            index++;
        }
        public override string ToString()
        {
            return "(" + TypeId + ") " + "CreateConst " + value;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            byte indice = TypeLinks[value.GetType()];
            writer.Write(indice);

            if (value is bool)
            {
                writer.Write((bool)value);
            }
            else if (value is int)
            {
                writer.Write((int)value);
            }
            else if (value is string)
            {
                writer.Write(value.ToString());
            }
            else
            {
                throw new NotImplementedException("Unknown const type.");
            }
        }
    }
}
