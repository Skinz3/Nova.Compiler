using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Nova.Bytecode.Enums;
using Nova.ByteCode.Enums;
using Nova.Members;
using Nova.Parser;
using Nova.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nova.IO
{
    public class NvFile
    {
        public string Filepath
        {
            get;
            private set;
        }
        public List<Using> Usings
        {
            get;
            private set;
        }
        public List<Class> Classes
        {
            get;
            private set;
        }
        public NvFile(string filePath)
        {
            this.Filepath = filePath;
            this.Usings = new List<Using>();
            this.Classes = new List<Class>();
        }
        /*
         * Add other verification (returns)
         */
        public bool Read()
        {
            string text = File.ReadAllText(Filepath); // replace by filestream

            var inputStream = new AntlrInputStream(text);
            var lexer = new NovaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new NovaParser(commonTokenStream);
            NovaParser.CompilationUnitContext ectx = parser.compilationUnit();

            ClassListener extractor = new ClassListener(this);
            ParseTreeWalker.Default.Walk(extractor, ectx);

            Console.WriteLine(ectx.ToStringTree(parser));

            return true;
        }

    }
}
