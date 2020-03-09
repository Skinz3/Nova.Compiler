using Nova.Utils;
using Nova.VirtualMachine.IO;
using Nova.VirtualMachine.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.VirtualMachine
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Logger.Write("You need to specify 1 .nov file.");
            }

            string fileName = args[0];

            NovFile file = new NovFile(fileName);

            if (!file.Deserialize())
            {
                return 1;
            }

            RuntimeContext context = new RuntimeContext(file);

            context.Initialize();

            Stopwatch st = Stopwatch.StartNew();

            context.CallMain();

            Logger.Write("Code executed in " + st.ElapsedMilliseconds + "ms");

            Console.Read();
            return 0;
        }
    }
}
