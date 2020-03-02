using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.ByteCode.Runtime
{
    public class Exec
    {
        public static void Execute(RuntimeContext context, object[] locales, List<ICode> bytecode)
        {
            int index = 0;
            Execute(context, locales, bytecode, ref index);
        }
        public static void Execute(RuntimeContext context, object[] locales, List<ICode> bytecode, ref int index)
        {
            while (index < bytecode.Count)
            {
                ICode element = bytecode[index];
                element.Compute(context, locales, ref index);
            }
        }

        public static void Run(NovFile novFile)
        {
            RuntimeContext context = new RuntimeContext(novFile);
            context.Initialize();
            var st = Stopwatch.StartNew();

            context.Call("ExampleClass", "Main", 0);

            Logger.Write("Program terminated in " + st.ElapsedMilliseconds + "ms", LogType.Success);

            if (context.StackSize > 0)
            {
                throw new Exception("Stack size is > 0.");
            }
        }
    }
}
