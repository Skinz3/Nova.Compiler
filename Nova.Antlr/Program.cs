using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Antlr
{
    class Program
    {
        private const string LexerPath = "NovaLexer.g4";
        private const string ParserPath = "NovaParser.g4";

        private const string OutputPath = @"C:\Users\Skinz\Desktop\Nova\Compiler\Nova\Antlr";

        static void Main(string[] args)
        {
            AntlrTool tool = new AntlrTool("antlr4-csharp-4.6.6-complete.jar");

            bool lexer = tool.Generate(LexerPath, OutputPath);
            bool parser = tool.Generate(ParserPath, OutputPath);

            if (lexer && parser)
            {
                Console.WriteLine("Antlr files generated.");
            }
            else
            {
                Console.WriteLine("Antlr generation ended with errors...");
            }
            Console.Read();
        }
    }
}
