using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Semantics
{
    public class SemanticalError
    {
        private string Filepath
        {
            get;
            set;
        }
        private string Message
        {
            get;
            set;
        }
        private int LineIndex
        {
            get;
            set;
        }
        public SemanticalError(string filepath, string message, int lineIndex)
        {
            this.Filepath = filepath;
            this.Message = message;
            this.LineIndex = lineIndex + 1; // starting from 1
        }
        public override string ToString()
        {
            return "File: " + Path.GetFileName(Filepath) + " "+Message+ " at line " + LineIndex;
        }
    }
}
