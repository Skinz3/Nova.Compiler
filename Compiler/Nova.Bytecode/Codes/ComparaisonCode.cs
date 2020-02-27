using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.ByteCode.Enums;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;

namespace Nova.ByteCode.Codes
{
    public class ComparaisonCode : ICode
    {
        private OperatorsEnum type;

        public int TypeId => 2;

        public ComparaisonCode(OperatorsEnum type)
        {
            this.type = type;
        }


        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            int val2 = (int)context.PopStack();
            int val1 = (int)context.PopStack();

            bool result = false;

            switch (type)
            {
                case OperatorsEnum.Inferior:
                    result = val1 < val2;
                    break;
                case OperatorsEnum.Different:
                    result = val1 != val2;
                    break;
                case OperatorsEnum.Superior:
                    result = val1 > val2;
                    break;
                case OperatorsEnum.Equals:
                    result = val1 == val2;
                    break;
                default:
                    throw new Exception();
            }

            context.PushStack(result == true ? 1 : 0);
            index++;
        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write((byte)type);
        }

        public override string ToString()
        {
            return "(" + TypeId + ") " + "Comparaison" + type;
        }
    }
}
