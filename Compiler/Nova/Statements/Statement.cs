﻿using Nova.ByteCode.Codes;
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

namespace Nova.Statements
{
    public abstract class Statement 
    {
        public string Input
        {
            get;
            private set;
        }
        protected IParentBlock Parent
        {
            get;
            private set;
        }
        public int LineIndex
        {
            get;
            private set;
        }

        public Statement(IParentBlock parent, string input, int lineIndex)
        {
            this.Parent = parent;
            this.Input = input;
            this.LineIndex = lineIndex;
        }
        public Statement(IParentBlock parent)
        {
            this.Parent = parent;
        }
        public virtual bool ValidateSyntax()
        {
            return true;
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}", this.GetType().Name, Input);
        }

        public virtual int GetLineSkip()
        {
            return 1;
        }

        public abstract void GenerateBytecode(ByteBlockMetadata context);

        public abstract void ValidateSemantics(SemanticsValidator validator);

    }
}
