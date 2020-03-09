using Nova.Utils;
using Nova.VirtualMachine.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine.Runtime
{
    class Exec
    {
        public static void Execute(RuntimeContext context, ByteMethod mainMethod) // byteMethod mainMethod as parameter
        {
            ByteMethod executingMethod = mainMethod;

            MethodCall call = new MethodCall(mainMethod, null, -1, null);
            context.CallStack.Add(call);

            List<int> ins = mainMethod.Block.Instructions;
            object[] locales = new object[mainMethod.Block.LocalesCount];

            int ip = 0;

            while (ip < ins.Count)
            {

                Logger.Write("op: " + ((OpCodes)ins[ip]).ToString(), LogType.Success);

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
                            Logger.Write(context.PopStack());
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

                        var targetMethod = context.NovFile.Classes[classId].Methods[methodId];

                        MethodCall call2 = new MethodCall(targetMethod, executingMethod, ip + 1, (object[])locales.Clone());

                        context.CallStack.Add(call2);

                        locales = new object[targetMethod.Block.LocalesCount];

                        for (int i = 0; i < locales.Length; i++)
                        {
                            locales[i] = context.PopStack();
                        }

                        ip = 0;
                        ins = targetMethod.Block.Instructions;

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
                        var lastCall = context.CallStack[context.CallStack.Count - 1];

                        if (context.CallStack.Count == 1)
                        {
                            return;
                        }
                        ip = lastCall.ReturnIp;
                        ins = lastCall.PreviousMethod.Block.Instructions;
                        locales = lastCall.PreviousLocales;

                        context.CallStack.RemoveAt(context.CallStack.Count - 1);
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
