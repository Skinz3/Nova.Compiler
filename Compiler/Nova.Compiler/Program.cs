using Nova.ByteCode;
using Nova.ByteCode.Generation;
using Nova.IO;
using Nova.Utils;
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
                Logger.Write("You need to specify at least one nova file (.nv).", LogType.Warning);
                Console.Read();
                return;
            }

            bool outputPathSpecified = Path.GetExtension(args.Last()) == Constants.INTERMEDIATE_LANGUAGE_FILE_EXTENSION;

            string outputPath;

            if (outputPathSpecified)
            {
                outputPath = args.Last();

                Logger.Write("Ouput path specified : " + outputPath, LogType.Debug);

                if (args.Length == 1)
                {
                    Console.Read(); // debug only, le programme ne doit pas être bloquant.
                    Logger.Write("You need to specify at least one nova file (.nv).", LogType.Warning);
                    return;
                }
            }
            else
            {
                Logger.Write("Using default ouput path : " + Constants.DEFAULT_OUTPUT_PATH, LogType.Debug);
                outputPath = Constants.DEFAULT_OUTPUT_PATH;
            }

            Stopwatch st = Stopwatch.StartNew();

            List<NvFile> files = new List<NvFile>();

            for (int i = 0; i < args.Length - (outputPathSpecified ? 1 : 0); i++)
            {
                NvFile file = new NvFile(args[i]);

                if (!file.Read() || !file.ReadClasses())
                {
                    Console.Read();
                    Environment.Exit(1);
                }
                files.Add(file);
                Logger.Write("File : " + args[i], LogType.Debug);
            }

            if (NovBuilder.Build(outputPath, files))
            {
                Logger.Write(outputPath + " generated in " + st.ElapsedMilliseconds + "ms");
                Console.Read();
            }
            else
            {
                Console.Read();
            }
        }
    }
}
