using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Runtime;
using Nova.ByteCode.Enums;
using Nova.ByteCode.Runtime;
using Nova.Utils.IO;

namespace Nova.ByteCode.Codes
{
    public class ArithmeticCode : ICode
    {
        public int OpId => 1;

        private OperatorsEnum type;

        public ArithmeticCode(OperatorsEnum type)
        {
            this.type = type;
        }


        public void Compute(RuntimeContext context,object[] locals, ref int index)
        {
            object val2 = context.PopStack();
            object val1 = context.PopStack();
            
            object result = 0;

            if (val1 is String || val2 is String)
            {
                switch (type)
                {
                    case OperatorsEnum.Plus:
                        result = val1.ToString() + val2;
                        break;
                    default:
                        throw new Exception();
                }
            }
            else
            {
                switch (type)
                {
                    case OperatorsEnum.Plus:
                        result = (int)val1 + (int)val2;
                        break;
                    case OperatorsEnum.Multiply:
                        result = (int)val1 * (int)val2;
                        break;
                    case OperatorsEnum.Minus:
                        result = (int)val1 - (int)val2;
                        break;
                    default:
                        throw new Exception();
                }
            }

            context.PushStack(result);
            index++;

        }

        public void Serialize(CppBinaryWriter writer)
        {
            writer.Write((int)type);
        }

        public override string ToString()
        {
            return "(" + OpId + ") " + "Arithmetic" + type;
        }

        public int GetSize()
        {
            return 1;
        }
    }
}
