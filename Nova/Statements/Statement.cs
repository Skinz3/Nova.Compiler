using Nova.ByteCode.Codes;
using Nova.ByteCode.Generation;
using Nova.ByteCode.IO;
using Nova.Lexer;
using Nova.IO;
using Nova.Members;
using Nova.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Bytecode.Symbols;
using Nova.Bytecode.Enums;

namespace Nova.Statements
{
    public abstract class Statement
    {
        public string Input
        {
            get;
            private set;
        }
        protected IChild Parent
        {
            get;
            private set;
        }
        public int Line
        {
            get;
            set;
        }
        public int LineIndex
        {
            get;
            private set;
        }

        public Statement(IChild parent, string input, int lineIndex)
        {
            this.Parent = parent;
            this.Input = input;
            this.LineIndex = lineIndex;
        }
        public Statement(IChild parent)
        {
            this.Parent = parent;
        }
  

        public override string ToString()
        {
            return string.Format("({0}) {1}", this.GetType().Name, Input);
        }

        public virtual int GetLineSkip()
        {
            return 1;
        }
        /*
         * Cette classe n'a rien a faire ici ! 
         */
        public abstract void GenerateBytecode(ClassesContainer container,ByteBlock context);

        public abstract void ValidateSemantics(SemanticsValidator validator);
    }
}
