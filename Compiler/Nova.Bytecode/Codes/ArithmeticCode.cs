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
    public class ArithmeticCode : ICode
    {
        public int TypeId => 1;

        private OperatorsEnum type;

        public ArithmeticCode(OperatorsEnum type)
        {
            this.type = type;
        }


        public void Compute(RuntimeContext context,ref object[] locals, ref int index)
        {
            int val2 = (int)context.PopStack();
            int val1 = (int)context.PopStack();

            int result = 0;

            switch (type)
            {
                case OperatorsEnum.Plus:
                    result = val1 + val2;
                    break;
                case OperatorsEnum.Multiply:
                    result = val1 * val2;
                    break;
                case OperatorsEnum.Minus:
                    result = val1 - val2;
                    break;
                default:
                    throw new Exception();
            }

            context.PushStack(result);
            index++;

        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write((byte)type);
        }

        public override string ToString()
        {
            return "ArithmeticOp " + type;
        }
    }
}
