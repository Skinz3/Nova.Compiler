using Nova.ByteCode;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Lexer;
using Nova.Utils;
using Nova.Utils.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Nova.Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Logger.Write("You need to specify a nova file (.nv).", LogType.Warning);
                Console.Read();
                return;
            }
            if (args.Length > 2)
            {
                Logger.Write("Args are [scriptPath] [outputPath]?");
                Console.Read();
                return;
            }

            bool outputPathSpecified = args.Length == 2;

            string outputPath;

            if (outputPathSpecified)
            {
                outputPath = args[1];
                Logger.Write("Output path specified : " + outputPath, LogType.Debug);

            }
            else
            {
                Logger.Write("Using default ouput path : " + Constants.DEFAULT_OUTPUT_PATH, LogType.Debug);
                outputPath = Constants.DEFAULT_OUTPUT_PATH;
            }

            Stopwatch st = Stopwatch.StartNew();

            NovBuilder builder = new NovBuilder(args[0], outputPath);

            if (!builder.Build())
            {
                Console.Read();
                return;
            }

            builder.Save();

            Logger.Write(outputPath + " generated in " + st.ElapsedMilliseconds + "ms");

            builder.PrintMainByteCode();

            Console.Read();

        }
    }
}
