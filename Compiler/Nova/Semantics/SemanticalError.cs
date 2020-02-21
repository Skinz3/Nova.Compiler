using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Semantics
{
    public class SemanticalError
    {
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
        public SemanticalError(string message, int lineIndex)
        {
            this.Message = message;
            this.LineIndex = lineIndex + 1; // starting from 1
        }
        public override string ToString()
        {
            return Message + " at line " + LineIndex;
        }
    }
}
