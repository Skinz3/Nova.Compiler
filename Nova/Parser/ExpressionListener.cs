using Nova.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Parser
{
    public class ExpressionListener : NovaParserBaseListener
    {
        private Expression Result
        {
            get;
            set;
        }
    }
}
