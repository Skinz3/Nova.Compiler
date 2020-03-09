using Nova.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Runtime
{
    class Exec
    {
        public static void Execute(RuntimeContext context, object[] locales, List<int> ins) // byteMethod mainMethod as parameter
        {
            int ip = 0;

            while (ip < ins.Count)
            {
                switch ((OpCodes)ins[ip])
                {
                    case OpCodes.Add:
                        context.PushStack((int)context.PopStack() + (int)context.PopStack());
                        ip++;
                        break;
                    case OpCodes.Div:
                        {
                            int val1 = (int)context.PopStack();
                            int val2 = (int)context.PopStack();
                            context.PushStack(val2 / val1);
                            ip++;
                            break;
                        }
                    case OpCodes.Printl:
                        {
                            Console.WriteLine(context.PopStack());
                            ip++;
                            break;
                        }
                    case OpCodes.PushNull:
                        context.PushStack(RuntimeContext.NULL_VALUE);
                        ip++;
                        break;
                    case OpCodes.PushConst:
                        context.PushStack(context.GetConstant(ins[++ip]));
                        ip++;
                        break;
                    case OpCodes.MethodCall:
                        int classId = ins[++ip];
                        int methodId = ins[++ip];
                        context.Call(classId, methodId);
                        ip++;
                        break;
                    case OpCodes.Load:
                        int id = ins[++ip];
                        context.PushStack(locales[id]);
                        ip++;
                        break;
                    case OpCodes.Mul:
                        context.PushStack((int)context.PopStack() * (int)context.PopStack());
                        ip++;
                        break;
                    case OpCodes.PushInt:
                        context.PushStack(ins[++ip]);
                        ip++;
                        break;
                    case OpCodes.Store:
                        locales[ins[++ip]] = context.PopStack();
                        ip++;
                        break;
                    case OpCodes.Return:
                        ip = ins.Count;
                        break;
                    case OpCodes.Sub:
                        {
                            int val1 = (int)context.PopStack();
                            int val2 = (int)context.PopStack();
                            context.PushStack(val2 - val1);
                            ip++;
                            break;
                        }
                    default:
                        Logger.Write("Unknown op code: " + ((OpCodes)ins[ip]).ToString(), LogType.Warning);
                        ip++;
                        break;
                }
            }
        }
    }
}
