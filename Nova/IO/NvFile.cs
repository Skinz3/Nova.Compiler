﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Nova.Bytecode.Enums;
using Nova.Bytecode.Symbols;
using Nova.ByteCode.Enums;
using Nova.Members;
using Nova.Parser;
using Nova.Parser.Errors;
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

        //  Console.Write(ectx.ToStringTree(parser));
        public bool Read()
        {

            string text = File.ReadAllText(Filepath);

            NovaParsingErrorHandler parsingErrorHandler = new NovaParsingErrorHandler();

            var inputStream = new AntlrInputStream(text);
            var lexer = new NovaLexer(inputStream);
            lexer.RemoveErrorListener(ConsoleErrorListener<int>.Instance);

            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new NovaParser(commonTokenStream);
            parser.RemoveErrorListener(ConsoleErrorListener<IToken>.Instance);
            parser.AddErrorListener(parsingErrorHandler);



            NovaParser.CompilationUnitContext ectx = parser.compilationUnit();

            ClassListener classListener = new ClassListener(this);

            foreach (var typeDeclaration in ectx.typeDeclaration())
            {
                typeDeclaration.EnterRule(classListener);
            }

            return parsingErrorHandler.ErrorsCount == 0;
        }

    }
}
